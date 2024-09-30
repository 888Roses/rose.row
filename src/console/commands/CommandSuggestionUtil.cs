using rose.row.console.commands.arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace rose.row.console.commands
{
    public static class CommandSuggestionUtil
    {
        public static List<SuggestionPair> pullSuggestionsFromEnum(
            string argumentName,
            Type enumType,
            string command,
            string lastArgument
        )
        {
            var list = new List<SuggestionPair>();

            foreach (var value in Enum.GetNames(enumType))
            {
                var valueBuilder = new StringBuilder();
                var commandWithoutLastArgument =
                    command.Remove(command.Length - lastArgument.Length - 1, lastArgument.Length + 1);
                valueBuilder
                    .Append(commandWithoutLastArgument)
                    .Append(" ")
                    .Append(argumentName)
                    .Append(":")
                    .Append(value);

                list.Add(new SuggestionPair(value, valueBuilder.ToString()));
            }

            return list;
        }
    }
}