using UnityEngine;

namespace rose.row.data
{
    public interface IConstantHolder { }

    public class ConstantHolder<T> : IConstantHolder
    {
        public string name;
        public string description;
        public T defaultValue;

        public ConstantHolder(string name, string description, T defaultValue)
        {
            this.name = name;
            this.description = description;
            this.defaultValue = defaultValue;

            Constants.defaultValues.Add(name, defaultValue);

            Debug.Log($"Registered constant value '{name}' with default: {defaultValue}.");
        }

        public T get()
        {
            if (Constants.defaultValues.ContainsKey(name))
                return (T) Constants.defaultValues[name];

            return defaultValue;
        }
    }
}
