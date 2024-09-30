using UnityEngine;

namespace rose.row.console.commands.arguments
{
    public class StringArgument : AbstractArgument
    {
        public string defaultValue;
        public string value;

        public StringArgument(string name, string defaultValue = "") : base(name)
        {
            this.defaultValue = defaultValue;
            value = defaultValue;
        }

        public override string expectedValue() => "String";

        public override void reset() => value = defaultValue;

        public override bool accept(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\"")
                || value.StartsWith("'") && value.EndsWith("'"))
                value = value.Remove(0, 1).Remove(value.Length - 2, 1);

            this.value = value;
            return true;
        }

        public override void throwError(string value)
        {
            Debug.LogError($"Invalid argument value {value} for argument {name}! Expected: String");
        }
    }
}