using rose.row.match;

namespace rose.row.console.commands
{
    public class WinCommand : AbstractConsoleCommand
    {
        public override string root => "win";
        public override string description => "Wins the current game.";

        public override void execute()
        {
            VictoryUi.EndGame(CurrentMatch.playerTeam, true);
        }
    }
}
