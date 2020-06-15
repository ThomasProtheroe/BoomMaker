using System;

namespace BoomMaker
{
    class Component
    {
        //Base Attributes;
        private string type;
        private float damage;
        private float shots;
        private float rateOfFire;
        private float deviation;
        private float recoilRate;
        private float recoilControl;
        private float velocity;
        private float capacity;

        public float[] getAttributes() {
            float[] attributes = new float[8];
            attributes[BoomMaker.CONST_DAMAGE] = damage;
            attributes[BoomMaker.CONST_SHOTS] = shots;
            attributes[BoomMaker.CONST_ROF] = rateOfFire;
            attributes[BoomMaker.CONST_DEVIATION] = deviation;
            attributes[BoomMaker.CONST_RECOIL_RATE] = recoilRate;
            attributes[BoomMaker.CONST_RECOIL_CONTROL] = recoilControl;
            attributes[BoomMaker.CONST_VELOCITY] = velocity;
            attributes[BoomMaker.CONST_CAPACITY] = capacity;

            return attributes;
        }

        public string getType() {
            return type;
        }
    }
}