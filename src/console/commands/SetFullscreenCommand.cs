using rose.row.console.commands.arguments;
using System;
using UnityEngine;

namespace rose.row.console.commands
{
    public class SetFullscreenCommand : AbstractConsoleCommand
    {
        public const string k_ModeArgument = "mode";

        public override string root => "screen.set_fullscreen";
        public override string description => "Sets the fullscreen mode of the application.";

        public SetFullscreenCommand()
        {
            arguments = new AbstractArgument[]
            {
                new StringArgument(k_ModeArgument, "fullscreen")
                {
                    hasCustomSuggestions = true,
                    customSuggestions = (string command, string lastArgument) =>
                    {
                        return CommandSuggestionUtil.pullSuggestionsFromEnum(
                            argumentName: k_ModeArgument,
                            enumType: typeof(FullScreenMode),
                            command: command,
                            lastArgument: lastArgument
                        );
                    }
                }
            };
        }

        public override void execute()
        {
            var modeArg = this[k_ModeArgument] as StringArgument;
            var mode = (FullScreenMode) Enum.Parse(
                enumType: typeof(FullScreenMode),
                value: modeArg.value
            );

            Screen.fullScreenMode = mode;
        }
    }
}