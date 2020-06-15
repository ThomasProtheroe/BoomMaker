using System;
using System.Collections.Generic;

namespace BoomMaker
{
    static class BoomMaker
    {
        //Constants
        public const int CONST_DAMAGE = 0;
        public const int CONST_SHOTS = 1;
        public const int CONST_ROF = 2;
        public const int CONST_DEVIATION = 3;
        public const int CONST_RECOIL_RATE = 4;
        public const int CONST_RECOIL_CONTROL = 5;
        public const int CONST_VELOCITY = 6;
        public const int CONST_CAPACITY = 7;

        //Component Data
        private static Dictionary<string, List<Component>> componentsData;

        public static Gun BuildGun(string[] filter=null) {
            if (componentsData.Count == 0) {
                loadComponentsData();
            }

            Gun newGun = new Gun();
            try {
                foreach(string componentType in newGun.getRequiredComponents()) {
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
        }
    }
}