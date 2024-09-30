using UnityEngine;

namespace rose.row.console.commands
{
    public class UnlockSwordCommand : AbstractConsoleCommand
    {
        public override string root => "item.unlock.sword";
        public override string description => "Unlocks the special hidden Sword item.";

        public override void execute()
        {
            WeaponManager.UnlockSecretHalloweenWeapon();
            Debug.Log("Unlocked sword.");
        }
    }
}