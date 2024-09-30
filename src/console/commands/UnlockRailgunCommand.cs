namespace rose.row.console.commands
{
    public class UnlockRailgunCommand : AbstractConsoleCommand
    {
        public override string root => "item.unlock.railgun";
        public override string description => "Unlocks the special hidden railgun item.";

        public override void execute()
        {
            WeaponManager.UnlockSecretWeapon();
        }
    }
}
