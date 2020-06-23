using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BoomMaker
{
    static class BoomMaker
    {
        //Constants
        public const int CONST_FIREMODE_NONE = 0;
        public const int CONST_FIREMODE_SEMI = 1;
        public const int CONST_FIREMODE_BURST = 2;
        public const int CONST_FIREMODE_AUTO = 3;

        //Component Data
        private static Dictionary<string, List<Component>> componentsData = new Dictionary<string, List<Component>>();
        private static string[] requiredComponents = {"body", "barrel", "grip", "ammunition"};

        public static Gun BuildGun(string[] filter=null) {
            if (componentsData.Count == 0) {
                loadCoreComponentsData();
            }

            Gun newGun = new Gun();
            List<string> newTypesRequired = new List<string> ();
            try {
                foreach(string componentType in requiredComponents) {
                    //Select a component of the correct type from those available
                    Component newComponent = getRandomComponentOfType(componentType);
                    newGun.addComponent(newComponent);

                    if (newComponent.hasRequiredTypes()) {
                        newTypesRequired = newTypesRequired.Union(newComponent.getRequiredTypes()).ToList();
                    }
                }

                foreach(string componentType in newTypesRequired) {
                    //Add any required additional component types
                    Component newComponent = getRandomComponentOfType(componentType);
                    newGun.addComponent(newComponent);
                }
            } catch (Exception e) {
                Console.WriteLine("Unable to find all needed component type files: {0}", e.Message);
            }

            newGun.calculateCoreAttributes();
            
            return newGun;
        }

        static Component getRandomComponentOfType(string componentType) {
            if (!componentsData.ContainsKey(componentType)) {
                loadComponentData(componentType);
            }

            List<Component> possibleComponents = new List<Component>();
            bool success = componentsData.TryGetValue(componentType, out possibleComponents);
            if (success == false) {
                throw new System.Exception("Missing File for component type: " + componentType);
            }

            Random random = new Random();
            Component newComponent = possibleComponents[random.Next(0, possibleComponents.Count)];
            return newComponent;
        }

        static void loadCoreComponentsData() {
            //Read component files for each component type and store into dictionary
            foreach(string componentType in requiredComponents) {
                loadComponentData(componentType);
            }
        }

        static void loadComponentData(string componentType) {
            List<Component> tempComponents = new List<Component> ();
            string path = "Data/" + componentType + ".json";

            var jsonText = System.IO.File.ReadAllText(@path);
            JArray data = (JArray)JsonConvert.DeserializeObject(jsonText);

            //Loop through objects in the JSON array and build a component from each
            foreach(var jsonComponent in data.Children()) {
                Component newComponent = new Component(componentType);
                newComponent.setName((string)jsonComponent.SelectToken("name"));
                if (jsonComponent.SelectToken("attributes") != null) {
                    foreach(JProperty property in jsonComponent.SelectToken("attributes").Children<JProperty>()) {
                        newComponent.setAttribute(property.Name, (float)property.Value);
                    }
                }

                if (jsonComponent.SelectToken("operators") != null) {
                    foreach(JProperty property in jsonComponent.SelectToken("operators").Children<JProperty>()) {
                        newComponent.setAttributeOperator(property.Name, (string)property.Value);
                    }
                }

                if (jsonComponent.SelectToken("tags") != null) {
                    List<string> tagList = new List<string> ();
                    tagList.AddRange(jsonComponent["tags"].ToObject<string[]>());
                    newComponent.addTags(tagList);
                }

                if (jsonComponent.SelectToken("requirements") != null) {
                    List<string> typeList = new List<string> ();
                    typeList.AddRange(jsonComponent["requirements"]["requiredTypes"].ToObject<string[]>());
                    newComponent.addRequiredTypes(typeList);
                }
                
                tempComponents.Add(newComponent);
            }

            //Add the finished component list to the component dictionary
            componentsData.Add(componentType, tempComponents);
        }
    }
}