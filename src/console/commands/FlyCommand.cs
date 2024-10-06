using rose.row.dev.dev_editor;
using UnityEngine;

namespace rose.row.console.commands
{
    public class FlyCommand : AbstractConsoleCommand
    {
        public override string root => "fly";
        public override string description => "Enables or disables fly mode.";

        public override void execute()
        {
            FlyMode.toggleFlying();

            Debug.Log(DevMainInfo.isFlying
                ? "Enabled fly mode."
                : "Disabled fly mode."
            );
        }
    }
}
