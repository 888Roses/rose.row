using rose.row.data.factions;

namespace rose.row.ui.ingame.scoreboard
{
    public class PlayerInfo
    {
        public int rank;
        public AbstractFaction faction;
        public int ping;

        public string name;
        public string squad;

        public int score;
        public int captures;
        public int kills;
        public int deaths;
        public int headshots;

        public int destroyedTanks;
        public int destroyedPlanes;
    }
}