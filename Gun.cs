using System.Collections.Generic;

namespace BoomMaker
{
    class Gun
    {
        //Gun Components
        protected Dictionary<string, Component> components;
        protected List<Mod> mods;
        protected string[] requiredComponents = {"body", "barrel", "grip", "stock", "ammunition"};
        protected string[] optionalComponents = {"scope"};

        //Core Attributes
        private float damage = 0;
        private float shots = 0;
        private float rateOfFire = 0;
        private float deviation = 0;
        private float recoilRate = 0;
        private float recoilControl = 0;
        private float velocity = 0;
        private float capacity = 0;

        //Variable Attributes
        private float recoil;
        private float ammoRemaining;

        public void calculateCoreAttributes() {
            foreach(KeyValuePair<string, Component> component in components) {
                addAttributesToCore(component.Value.getAttributes());
            }
        }

        private void addAttributesToCore(float[] attributes) {
            damage += attributes[BoomMaker.CONST_DAMAGE];
            shots += attributes[BoomMaker.CONST_SHOTS];
            rateOfFire += attributes[BoomMaker.CONST_ROF];
            deviation += attributes[BoomMaker.CONST_DEVIATION];
            recoilRate += attributes[BoomMaker.CONST_RECOIL_RATE];
            recoilControl += attributes[BoomMaker.CONST_RECOIL_CONTROL];
            velocity += attributes[BoomMaker.CONST_VELOCITY];
            capacity += attributes[BoomMaker.CONST_CAPACITY];
        }

        public void addComponent(Component newComponent) {
            string type = newComponent.getType();
            if (components.ContainsKey(type)) {
                return;
            }

            components.Add(type, newComponent);
        }

        public string[] getRequiredComponents() {
            return requiredComponents;
        }
    }
}
