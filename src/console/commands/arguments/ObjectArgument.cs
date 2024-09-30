using UnityEngine;

namespace rose.row.console.commands.arguments
{
    public class ObjectArgument : AbstractArgument
    {
        public object defaultValue;
        public object value;

        public ObjectArgument(string name, object defaultValue = null) : base(name)
        {
            this.defaultValue = defaultValue;
            value = defaultValue;
        }

        public override string expectedValue() => "Any";

        public override void reset() => value = defaultValue;

        public override bool accept(string value)
        {
            this.value = value;
            return true;
        }

        public override void throwError(string value)
        {
            Debug.LogError($"Invalid argument value {value} for argument {name}! Expected: Any");
        }
    }
}