using System;
using System.Collections.Generic;
using System.Linq;

namespace BoomMaker
{
    class Component
    {
        //Attributes
        private string type;
        private string name;
        private Dictionary<string, float> attributes;
        private List<string> tags;
        private List<string> requiredTypes;
        private List<string> excludedTags;

        //Attribute operators
        private Dictionary<string, string> attributeOperators;

        public Component(string inType) {
            type = inType;
            attributes = new Dictionary<string, float> ();
            attributeOperators = new Dictionary<string, string> ();
            tags = new List<string> ();
            requiredTypes = new List<string> ();
            excludedTags = new List<string> ();
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

        public void addTag(string tag){
            tags.Add(tag);
        }

        public void addTags(List<string> inTags) {
            tags = tags.Union(inTags).ToList();
        }

        public bool hasTag(string tag) {
            if (tags.Contains(tag)) {
                return true;
            }
            return false;
        }

        public string getType() {
            return type;
        }

        public string getName() {
            return name;
        }

        public void setName(string inName) {
            name = inName;
        }

        public List<string> getRequiredTypes() {
            return requiredTypes;
        }

        public bool hasRequiredTypes() {
            if (requiredTypes.Count > 0) {
                return true;
            }
            return false;
        }

        public void addRequiredTypes(List<string> inTypes) {
            requiredTypes = requiredTypes.Union(inTypes).ToList();
        }
    }
}