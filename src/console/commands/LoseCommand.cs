using rose.row.match;

namespace rose.row.console.commands
{
    public class LoseCommand : AbstractConsoleCommand
    {
        public override string root => "lose";
        public override string description => "Loses the current game.";

        public override void execute()
        {
            // TODO: Change this to allow 3 factions.
            VictoryUi.EndGame(CurrentMatch.playerTeam == 0 ? 1 : 0, true);
        }
    }
}
