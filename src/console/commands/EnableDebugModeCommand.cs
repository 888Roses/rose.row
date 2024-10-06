using rose.row.dev.dev_editor;
using UnityEngine;

namespace rose.row.console.commands
{
    public class EnableDebugModeCommand : AbstractConsoleCommand
    {
        public override string root => "debug";
        public override string description => "Enables or disables debug mode.";

        public override void execute()
        {
            DevMainInfo.isDebugEnabled = !DevMainInfo.isDebugEnabled;
            IngameDebugGizmos.instance.enabled = DevMainInfo.isDebugEnabled;
            Debug.Log($"{(DevMainInfo.isDebugEnabled ? "Enabled" : "Disabled")} debug gizmos.");
        }
    }
}
