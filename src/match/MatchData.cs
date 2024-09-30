using rose.row.data.factions;
using rose.row.main_menu.ui.desktop.war.missions;

namespace rose.row.match
{
    public static class CurrentMatch
    {
        public static int playerTeam;

        public static AbstractFaction enemyFaction;
        public static AbstractFaction playerFaction => Factions.playerFaction;

        public static AbstractFaction getFactionOfTeam(int team)
        {
            //Debug.Log(enemyFaction);
            //Debug.Log(playerFaction);

            if (team == -1)
            {
                return null;
            }

            if (team == playerTeam)
            {
                return playerFaction;
            }

            return enemyFaction;
        }

        public static WarMission mission;
    }
}