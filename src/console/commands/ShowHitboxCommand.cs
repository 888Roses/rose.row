using rose.row.dev.dev_editor;
using UnityEngine;

namespace rose.row.console.commands
{
    public class ShowHitboxCommand : AbstractConsoleCommand
    {
        public override string root => "debug.hitbox";
        public override string description => "Turns on or off hitboxes display.";

        public override void execute()
        {
            DevMainInfo.showHitboxes = !DevMainInfo.showHitboxes;

            if (DevMainInfo.showHitboxes)
                Debug.Log($"Enabled hitbox display.");
            else
                Debug.Log($"Disabled hitbox display.");
        }
    }

    public class ShowBonesCommand : AbstractConsoleCommand
    {
        public override string root => "debug.bones";
        public override string description => "Turns on or off bones display.";

        public override void execute()
        {
            DevMainInfo.showBones = !DevMainInfo.showBones;

            if (DevMainInfo.showBones)
                Debug.Log($"Enabled bones display.");
            else
                Debug.Log($"Disabled bones display.");
        }
    }
}
