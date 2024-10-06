using rose.row.dev.dev_editor;
using UnityEngine;

namespace rose.row.console.commands
{
    public class ShowPickupableWeaponBoxCommand : AbstractConsoleCommand
    {
        public override string root => "debug.pickupable_weapon_bounds";
        public override string description => "Turns on or off pickupable weapons bound display.";

        public override void execute()
        {
            DevMainInfo.showPickupableWeaponBoxes = !DevMainInfo.showPickupableWeaponBoxes;

            if (DevMainInfo.showPickupableWeaponBoxes)
                Debug.Log($"Enabled pickupable weapons bounds display.");
            else
                Debug.Log($"Disabled pickupable weapons bounds display.");
        }
    }
}
