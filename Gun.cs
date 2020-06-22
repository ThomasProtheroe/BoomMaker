using System;
using System.Collections.Generic;

namespace BoomMaker
{
    class Gun
    {
        //Gun Components
        protected Dictionary<string, Component> components;
        protected List<Mod> mods;

        //Core Attributes
        public  float damage = 0;
        public float armorPiercing = 0;
        public float shots = 0;
        public float rateOfFire = 0;
        public float deviation = 0;
        public float recoilRate = 0;
        public float recoilControl = 0;
        public float recoilCap = 0;
        public float handlingRecoilRate = 0;
        public float handlingRecoilControl = 0;
        public float handlingRecoilCap = 0;
        public float movementRecoilRate = 0;
        public float movementRecoilControl = 0;
        public float movementRecoilCap = 0;
        public float velocity = 0;
        public float capacity = 0;
        public int firingMode = 0;

        //Variable Attributes
        private float recoil;
        private float handlingRecoil;
        private float movementRecoil;
        private float ammoRemaining;

        public Gun() {
            components = new Dictionary<string, Component> ();
        }

        public void calculateCoreAttributes() {
            foreach(KeyValuePair<string, Component> component in components) {
                foreach(KeyValuePair<string, float> attribute in component.Value.getAttributes()) {
                    addAttributeToCore(attribute.Key, attribute.Value, component.Value.getAttributeOperator(attribute.Key));
                }
            }
        }

        private void addAttributeToCore(string attribute, float compValue, string attributeOperator) {
            //Deal with attributes requiring special handling
            if (attribute == "firingMode") {
                firingMode = (int)compValue;
                return;
            }
            //Get the correct field to add the attribute to
            var currentVal = this.GetType().GetField(attribute).GetValue(this);
            float newVal = 0;
            switch(attributeOperator) {
                case "+":
                    newVal = (float)currentVal + compValue;
                    this.GetType().GetField(attribute).SetValue(this, newVal);
                break;
                case "-":
                    newVal = (float)currentVal - compValue;
                    this.GetType().GetField(attribute).SetValue(this, newVal);
                break;
                case "*":
                    newVal = (float)currentVal * compValue;
                    this.GetType().GetField(attribute).SetValue(this, newVal);
                break;
                case "/":
                    newVal = (float)currentVal / compValue;
                    this.GetType().GetField(attribute).SetValue(this, newVal);
                break;
            }
        }

        public void addComponent(Component newComponent) {
            string type = newComponent.getType();
            if (components.ContainsKey(type)) {
                return;
            }

            components.Add(type, newComponent);
        }

        public string getComponentsString() {
            string compString = "Components:";
            foreach(KeyValuePair<string, Component> comp in components) {
                compString += "\n" + comp.Value.getName();
            }
            return compString;
        }

        public string getAttributesString() {
            string display = "";
            display += "Damage: " + damage;
            display += "\nAP: " + armorPiercing;
            display += "\nShots: " + shots;
            display += "\nRate of Fire: " + rateOfFire;
            display += "\nDeviation: " + deviation;
            display += "\nRecoil: " + recoilRate + "/" + recoilControl + "/" + recoilCap;
            display += "\nHandling: " + handlingRecoilRate + "/" + handlingRecoilControl + "/" + handlingRecoilCap;
            display += "\nMovement Control: " + movementRecoilRate + "/" + movementRecoilControl + "/" + movementRecoilCap;
            display += "\nVelocity: " + velocity;
            display += "\nCapacity: " + capacity;
            display += "\nFiring Mode: ";
            if (firingMode == 1) {
                display += "Semi Automatic";
            } else if (firingMode == 2) {
                display += "Burst Fire";
            } else if (firingMode == 3) {
                display += "Automatic";
            } else {
                display += "None";
            }

            return display;
        }
    }
}
