using rose.row.data.factions;
using rose.row.main_menu.war.war_data;
using rose.row.match;

namespace rose.row.util
{
    public static class CityInfoUtil
    {
        public static AbstractFaction getFaction(this CityInfo city)
        {
            return Factions.getFromISO2(city.iso2);
        }
    }
}