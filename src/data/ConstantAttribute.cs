using System;

namespace rose.row.data
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ConstantAttribute : Attribute
    {
        public string name;
        public string description;
        public object defaultValue;

        public ConstantAttribute(string name, string description, object defaultValue)
        {
            this.name = name;
            this.description = description;
            this.defaultValue = defaultValue;
        }
    }
}
