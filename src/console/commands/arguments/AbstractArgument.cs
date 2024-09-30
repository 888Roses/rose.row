using System;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.console.commands.arguments
{
    public readonly struct SuggestionPair
    {
        public readonly string displayed;
        public readonly string value;

        public SuggestionPair(string displayed, string value)
        {
            this.displayed = displayed;
            this.value = value;
        }
    }

    public abstract class AbstractArgument
    {
        public string name;

        public bool hasCustomSuggestions;
        public Func<string, string, IEnumerable<SuggestionPair>> customSuggestions;

        public AbstractArgument(string name)
            : this(name, null)
        {
            hasCustomSuggestions = false;
        }

        public AbstractArgument(string name, Func<string, string, IEnumerable<SuggestionPair>> customSuggestions)
        {
            this.name = name;
            hasCustomSuggestions = true;
            this.customSuggestions = customSuggestions;
        }

        public abstract string expectedValue();

        public abstract void reset();

        public abstract bool accept(string value);

        public virtual void throwError(string value)
        {
            Debug.LogError($"Invalid argument value {value} for argument {name}!");
        }
    }
}