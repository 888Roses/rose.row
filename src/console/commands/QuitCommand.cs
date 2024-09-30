using UnityEngine;

namespace rose.row.console.commands
{
    public class QuitCommand : AbstractConsoleCommand
    {
        public override string root => "quit";
        public override string description => "Quits the application.";

        public override void execute()
        {
            Application.Quit();
        }
    }
}