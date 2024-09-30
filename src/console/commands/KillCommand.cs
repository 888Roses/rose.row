namespace rose.row.console.commands
{
    public class KillCommand : AbstractConsoleCommand
    {
        public override string root => "kill";
        public override string description => "Kills the player.";

        public override void execute()
        {
            FpsActorController.instance.actor.Kill(DamageInfo.Default);
        }
    }
}