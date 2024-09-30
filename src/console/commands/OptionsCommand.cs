namespace rose.row.console.commands
{
    public class OptionsCommand : AbstractConsoleCommand
    {
        public override string root => "options";
        public override string description => "Shows the option menu.";

        public override void execute()
        {
            Options.Show();
        }
    }
}