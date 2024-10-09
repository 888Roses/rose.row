using rose.row.console.commands;
using rose.row.console.commands.arguments;
using rose.row.easy_package.ui.text;
using rose.row.ui.console.elements.autocompletion;
using rose.row.util;

namespace rose.row.ui.console.elements.inputfield
{
    public struct CommandSuggestionProvider
    {
        public static string getPrettyCommandName(AbstractConsoleCommand command)
        {
            var builder = new TextComponent(command.root);
            builder.setStyle(TextStyle.empty.withColor(ConsoleColors.autoCompletionSuggestionText));

            var description = new TextComponent(" " + command.description);
            description.setStyle(TextStyle.empty.withColor(ConsoleColors.autoCompletionSuggestionDescription));

            return builder.append(description).getString();
        }

        public static bool isOutOfQuotation(string value)
        {
            if (!value.Contains("\"") && !value.Contains("'"))
                return true;

            var doubleQuotesCount = 0;
            var simpleQuotesCount = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '"')
                    doubleQuotesCount++;

                if (value[i] == '\'')
                    simpleQuotesCount++;
            }

            return (doubleQuotesCount == 0 || doubleQuotesCount % 2 == 0) && (simpleQuotesCount == 0 || simpleQuotesCount % 2 == 0);
        }

        public static void populateSuggestedCommands(
            string value,
            AutoCompletionElement completion
        )
        {
            completion.clearSuggestionItems();

            string getPrettyArgumentName(AbstractArgument arg)
            {
                return $"{arg.name}:{arg.expectedValue()}";
            }

            void createSuggestion(AbstractArgument argument, string lastArgument, string prettyName)
            {
                var @base = getSuggestionRoot(lastArgument) + " ";
                var pretty = $"{@base}{prettyName}";
                var functional = $"{@base}{argument.name}:";
                completion.createSuggestionItem(pretty, functional);
            }

            var valueTrimmed = value.Trim();
            string getSuggestionRootWithBase(string baseValue, string lastArgument)
                => baseValue.Remove(baseValue.Length - lastArgument.Length - 1, lastArgument.Length + 1);
            string getSuggestionRoot(string lastArgument) => getSuggestionRootWithBase(valueTrimmed, lastArgument);

            var consoleCommand = Console.getCommandWithRoot(valueTrimmed);

            if (valueTrimmed.Contains(" "))
            {
                var splits = valueTrimmed.safeSplitWhitespace();
                var root = splits[0].Trim();
                splits.RemoveAt(0);
                var lastArgument = splits[splits.Count - 1];

                if (consoleCommand == null)
                    consoleCommand = Console.getCommandWithRoot(root);

                foreach (var argument in consoleCommand.arguments)
                {
                    #region check that argument isn't already used before suggesting it.

                    var isArgumentAlreadyUsed = false;
                    foreach (var currentSplit in splits)
                    {
                        // We don't want to apply this logic to the actual split that the user might be writing to.
                        if (currentSplit == lastArgument)
                            continue;

                        if (currentSplit.Contains(":"))
                        {
                            if (currentSplit.Split(':')[0] == argument.name)
                            {
                                isArgumentAlreadyUsed = true;
                                break;
                            }
                        }
                    }

                    if (isArgumentAlreadyUsed)
                        continue;

                    #endregion check that argument isn't already used before suggesting it.

                    #region creating the argument suggestion if we started writing it.

                    var prettyName = getPrettyArgumentName(argument);
                    if ((lastArgument.StartsWith(argument.name) || argument.name.StartsWith(lastArgument))
                        && prettyName != lastArgument)
                    {
                        #region suggesting custom suggestions if there's any

                        var hasGivenCustomSuggestions = false;
                        if (argument.hasCustomSuggestions && lastArgument.StartsWith(argument.name + ":"))
                        {
                            // Only suggest things if it has past the :
                            if (lastArgument.Contains(":"))
                            {
                                hasGivenCustomSuggestions = true;
                                foreach (var suggestion in argument.customSuggestions(valueTrimmed, lastArgument))
                                {
                                    completion.createSuggestionItem(suggestion.displayed + " ", suggestion.value);
                                }
                            }
                        }

                        #endregion suggesting custom suggestions if there's any

                        #region normal suggestions

                        if (!hasGivenCustomSuggestions)
                        {
                            createSuggestion(argument, lastArgument, prettyName);
                        }

                        #endregion normal suggestions
                    }

                    #endregion creating the argument suggestion if we started writing it.
                }

                #region giving a list of all available arguments if we're not writing one.

                if (splits.Count > 0 && value.EndsWith(" ") && isOutOfQuotation(valueTrimmed))
                {
                    foreach (var argument in consoleCommand.arguments)
                    {
                        #region making sure that the suggested argument isn't already being used.

                        var isArgumentAlreadyUsed = false;
                        foreach (var currentSplit in splits)
                        {
                            if (currentSplit.Contains(":"))
                            {
                                if (currentSplit.Split(':')[0] == argument.name)
                                {
                                    isArgumentAlreadyUsed = true;
                                    break;
                                }
                            }
                        }

                        if (isArgumentAlreadyUsed)
                            continue;

                        #endregion making sure that the suggested argument isn't already being used.

                        #region suggesting the argument

                        completion.createSuggestionItem(
                            valueTrimmed + " " + getPrettyArgumentName(argument),
                            valueTrimmed + " " + argument.name + ":");

                        #endregion suggesting the argument
                    }
                }

                #endregion giving a list of all available arguments if we're not writing one.
            }
            else
            {
                if (consoleCommand == null)
                {
                    foreach (var command in Console.consoleCommands)
                    {
                        if (command.root.StartsWith(value))
                        {
                            completion.createSuggestionItem(getPrettyCommandName(command), command.root);
                        }
                    }
                }
                // This means that the user actually has written the console command properly.
                else
                {
                    if (value.Contains(" "))
                    {
                        if (value.EndsWith(" "))
                        {
                            if (value == consoleCommand.root + " ")
                                return;

                            var splits = valueTrimmed.safeSplitWhitespace();
                            if (splits.Count > 0)
                            {
                                foreach (var argument in consoleCommand.arguments)
                                    completion.createSuggestionItem(
                                        value + getPrettyArgumentName(argument),
                                        value + argument.name + ":");

                                return;
                            }
                        }

                        foreach (var argument in consoleCommand.arguments)
                            completion.createSuggestionItem(
                                consoleCommand.root + " " + getPrettyArgumentName(argument),
                                consoleCommand.root + " " + argument.name + ":");
                    }
                }
            }
        }
    }
}