using HarmonyLib;

namespace rose.row.console.commands
{
    public class EnableDebugModeCommand : AbstractConsoleCommand
    {
        public override string root => "debug";
        public override string description => "Enables debug mode.";

        public override void execute()
        {
            Traverse.Create(GameManager.instance).Method("InitializeIngameDebugGizmos").GetValue();
        }
    }
}
