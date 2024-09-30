namespace rose.row.console.commands
{
    public class RestartCommand : AbstractConsoleCommand
    {
        public override string root => "restart";
        public override string description => "Restarts the current match.";

        public override void execute()
        {
            GameManager.RestartLevel();
        }
    }
}