using System;
using System.Collections.Generic;

namespace BoomMaker
{
    class Component
    {
        //Attributes
        private string type;
        private Dictionary<string, float> attributes;

        //Attribute operators
        private Dictionary<string, string> attributeOperators;

        public Component(string inType) {
            type = inType;
        }

        public Dictionary<string, float> getAttributes() {
            return attributes;
        }

        public float getAttribute(string attribute) {
            if (attributes.ContainsKey(attribute)) {
                return attributes[attribute];
            }

            return 0;
        }

        public void setAttribute(string attribute, float value) {
            attributes.Add(attribute, value);
        }

        public string getAttributeOperator(string attribute) {
            if (attributeOperators.ContainsKey(attribute)) {
                return attributeOperators[attribute];
            }

            return "+";
        }

        public void setAttributeOperator(string attribute, string op) {
            attributeOperators.Add(attribute, op);
        }

        public string getType() {
            return type;
        }
    }
}