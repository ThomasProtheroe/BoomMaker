using System.Collections.Generic;

namespace BoomMaker
{
    class Gun
    {
        //Gun Components
        protected Dictionary<string, Component> components;
        protected List<Mod> mods;

        //Core Attributes
        private float damage = 0;
        private float shots = 0;
        private float rateOfFire = 0;
        private float deviation = 0;
        private float recoilRate = 0;
        private float recoilControl = 0;
        private float velocity = 0;
        private float capacity = 0;
        private int firingMode = 0;

        //Variable Attributes
        private float recoil;
        private float ammoRemaining;

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
            }
            //Get the correct field to add the attribute to
            var currentVal = this.GetType().GetField(attribute).GetValue(this);
            float newVal;
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
    }
}
