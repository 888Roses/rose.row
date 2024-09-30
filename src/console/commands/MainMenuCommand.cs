namespace rose.row.console.commands
{
    public class MainMenuCommand : AbstractConsoleCommand
    {
        public override string root => "menu";
        public override string description => "Goes back to the main menu.";

        public override void execute()
        {
            GameManager.ReturnToMenu();
        }
    }
}