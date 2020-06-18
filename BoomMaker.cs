using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BoomMaker
{
    static class BoomMaker
    {
        //Constants
        public const int CONST_FIREMODE_NONE = 0;
        public const int CONST_FIREMODE_SEMI = 1;
        public const int CONST_FIREMODE_BURST = 2;
        public const int CONST_FIREMODE_AUTO = 3;

        public const int CONST_DAMAGE = 0;
        public const int CONST_SHOTS = 1;
        public const int CONST_ROF = 2;
        public const int CONST_DEVIATION = 3;
        public const int CONST_RECOIL_RATE = 4;
        public const int CONST_RECOIL_CONTROL = 5;
        public const int CONST_VELOCITY = 6;
        public const int CONST_CAPACITY = 7;
        public const int CONST_FIREMODE = 8;

        //Component Data
        private static Dictionary<string, List<Component>> componentsData;
        private static string[] requiredComponents = {"body", "barrel", "grip", "stock", "ammunition"};
        private static string[] optionalComponents = {"scope"};

        public static Gun BuildGun(string[] filter=null) {
            if (componentsData.Count == 0) {
                loadComponentsData();
            }

            Gun newGun = new Gun();
            try {
                foreach(string componentType in requiredComponents) {
                    //Select a component of the correct type from those available
                    List<Component> possibleComponents = new List<Component>();
                    bool success = componentsData.TryGetValue(componentType, out possibleComponents);
                    if (success == false) {
                        throw new System.Exception("Missing File for component type: " + componentType);
                    }

                    Random random = new Random();
                    newGun.addComponent(possibleComponents[random.Next(0, possibleComponents.Count)]);
                }
            } catch (Exception e) {
                Console.WriteLine("Unable to find all needed component type files: {0}", e.Message);
                throw;
            }
            

            return newGun;
        }

        static void loadComponentsData() {
            //Read component files for each component type and store into dictionary
            foreach(string componentType in requiredComponents) {
                List<Component> tempComponents = new List<Component> ();
                string path = "Data/" + componentType + ".json";

                var jsonText = System.IO.File.ReadAllText(@path);
                JObject jsonObject = JObject.Parse(jsonText);

                //Loop through objects in the JSON array and build a component from each
                Console.Write(jsonObject.Children());
                Component newComponent = new Component(componentType);

                //newComponent.setAttributes(valuesFloat);
                //tempComponents.Add(newComponent);

                //Add the finished component list to the component dictionary
                //componentsData.Add(componentType, tempComponents);
            }
        }
    }
}