using rose.row.dev.dev_editor;
using UnityEngine;

namespace rose.row.console.commands
{
    public class HitboxCommand : AbstractConsoleCommand
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
}
