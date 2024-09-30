using UnityEngine;

namespace rose.row.console.commands.arguments
{
    public class BooleanArgument : AbstractArgument
    {
        public bool defaultValue;
        public bool value;

        public BooleanArgument(string name, bool defaultValue = false) : base(name)
        {
            this.defaultValue = defaultValue;
            value = defaultValue;
        }

        public override string expectedValue() => "Boolean";

        public override void reset() => value = defaultValue;

        public override bool accept(string value) => bool.TryParse(value, out this.value);

        public override void throwError(string value)
        {
            Debug.LogError($"Invalid argument value {value} for argument {name}! Expected: Boolean");
        }
    }
}