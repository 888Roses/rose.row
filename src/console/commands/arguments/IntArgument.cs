using System.Globalization;
using UnityEngine;

namespace rose.row.console.commands.arguments
{
    public class IntArgument : AbstractArgument
    {
        public int defaultValue;
        public int value;

        public IntArgument(string name, int defaultValue = 1) : base(name)
        {
            this.defaultValue = defaultValue;
            value = defaultValue;
        }

        public override string expectedValue() => "Int";

        public override void reset() => value = defaultValue;

        public override bool accept(string value) => int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out this.value);

        public override void throwError(string value)
        {
            Debug.LogError($"Invalid argument value {value} for argument {name}! Expected: Int");
        }
    }
}