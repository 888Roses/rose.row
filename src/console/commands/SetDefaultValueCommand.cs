using rose.row.console.commands.arguments;
using rose.row.data;
using rose.row.easy_package.ui.text;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace rose.row.console.commands
{
    public class SetDefaultValueCommand : AbstractConsoleCommand
    {
        public const string k_CommandName = "set_value";
        public const string k_NameArgument = "name";
        public const string k_ValueArgument = "value";

        public override string root => k_CommandName;
        public override string description => "Sets a default in-game value such as whether the actors can fall down, etc.";

        public SetDefaultValueCommand()
        {
            arguments = new AbstractArgument[]
            {
                new StringArgument(k_NameArgument, "")
                {
                    hasCustomSuggestions = true,
                    customSuggestions = provideDefaultValueNames
                },
                new ObjectArgument(k_ValueArgument)
            };
        }

        public static string prepare(string text) => text.Replace(" ", "").ToLowerInvariant();

        private IEnumerable<SuggestionPair> provideDefaultValueNames(string command, string lastArgument)
        {
            var list = new List<SuggestionPair>();

            var preparedLastArgument = prepare(lastArgument.Replace($"{k_NameArgument}:", ""));
            var hasDoubleQuotes = preparedLastArgument.StartsWith("\"") && preparedLastArgument.EndsWith("\"");
            var hasSimpleQuotes = preparedLastArgument.StartsWith("'") && preparedLastArgument.EndsWith("'");
            if (hasDoubleQuotes || hasSimpleQuotes)
                preparedLastArgument = preparedLastArgument.Remove(0, 1).Remove(preparedLastArgument.Length - 2, 1);

            foreach (var defaultValue in Constants.defaultValues)
            {
                var preparedDefaultValueName = prepare(defaultValue.Key);

                if (preparedDefaultValueName.StartsWith(preparedLastArgument))
                {
                    var displayName = new TextComponent(defaultValue.Key);
                    var description = new TextComponent($" (current: {defaultValue.Value})");
                    var descriptionStyle = TextStyle.empty.withColor("#828282");
                    description.setStyle(descriptionStyle);
                    displayName.append(description);
                    var valueBuilder = new StringBuilder();
                    var commandWithoutLastArgument =
                        command.Remove(command.Length - lastArgument.Length - 1, lastArgument.Length + 1);
                    valueBuilder
                        .Append(commandWithoutLastArgument)
                        .Append(" ")
                        .Append(k_NameArgument)
                        .Append(":");

                    if (defaultValue.Key.Contains(" "))
                        valueBuilder.Append("\"");

                    valueBuilder.Append(defaultValue.Key);

                    if (defaultValue.Key.Contains(" "))
                        valueBuilder.Append("\"");

                    list.Add(new SuggestionPair(displayName.getString(), valueBuilder.ToString()));
                }

                if (list.Count > 15)
                    break;
            }

            return list;
        }

        public override void execute()
        {
            var nameArgument = this[k_NameArgument] as StringArgument;
            var valueArgument = this[k_ValueArgument] as ObjectArgument;

            if (string.IsNullOrEmpty(nameArgument.value) || !Constants.defaultValues.ContainsKey(nameArgument.value))
            {
                Debug.LogError($"The default value {nameArgument.value} could not be found or used!");
                return;
            }

            var currentDefaultValue = Constants.defaultValues[nameArgument.value];
            var newDefaultValue = valueArgument.value;

            try
            {
                var newValue = Convert.ChangeType(newDefaultValue, currentDefaultValue.GetType());
                Constants.defaultValues[nameArgument.value] = newValue;
                Debug.Log($"Successfully set {nameArgument.value} to {newValue}.");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                Debug.LogWarning($"Could not convert {newDefaultValue} as the required value for {nameArgument.value} ({currentDefaultValue.GetType().Name}).");
            }
        }
    }
}