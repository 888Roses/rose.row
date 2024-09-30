using rose.row.data.factions;
using rose.row.main_menu.war.war_data;
using rose.row.match;
using rose.row.util;
using System.Linq;

namespace rose.row.main_menu.ui.desktop.war.missions
{
    public class WarMission
    {
        public enum MissionType
        {
            Attack,
            Defense
        }

        public static WarMission generateRandom()
        {
            var mission = new WarMission();
            var city = WarCityDatabase.loadedCities.random();

            if (city.iso2 == Factions.playerFaction.factionISO2)
            {
                mission.type = MissionType.Defense;
            }
            else
            {
                mission.type = MissionType.Attack;
            }

            mission.city = city;
            return mission;
        }

        public CityInfo city;
        public MissionType type;

        public AbstractFaction getRandomFaction()
        {
            if (type == MissionType.Attack)
            {
                var result = city.getFaction();
                //Debug.Log($"Attack mode: {result == null}");
                return result;
            }

            var factionsWithoutPlayer = Factions.registeredFactions.ToList();
            factionsWithoutPlayer.Remove(Factions.playerFaction);
            var result2 = factionsWithoutPlayer.random();
            //Debug.Log($"Defense mode: {result2 == null} {factionsWithoutPlayer.Count}");
            return result2;
        }
    }
}