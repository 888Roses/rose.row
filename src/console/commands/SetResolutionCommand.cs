using rose.row.console.commands.arguments;
using UnityEngine;

namespace rose.row.console.commands
{
    public class SetResolutionCommand : AbstractConsoleCommand
    {
        public const string k_WidthArgument = "width";
        public const string k_HeightArgument = "height";

        public override string root => "screen.set_resolution";
        public override string description => "Sets the screen resolution to a new value.";

        public SetResolutionCommand()
        {
            arguments = new AbstractArgument[]
            {
                new IntArgument(k_WidthArgument, 1920),
                new IntArgument(k_HeightArgument, 1080),
            };
        }

        public override void execute()
        {
            var widthArg = this[k_WidthArgument] as IntArgument;
            var heightArg = this[k_HeightArgument] as IntArgument;

            Screen.SetResolution(
                width: widthArg.value,
                height: heightArg.value,
                fullscreen: Screen.fullScreen
            );
        }
    }
}