namespace rose.row.main_menu.war.war_data
{
    public static class WarDatabase
    {
        /// <summary>
        /// Contains the ISO2 names of the countries which's cities are supported by the war map.
        /// </summary>
        public static readonly string[] countries = new string[]
        {
            "fr", "gb", "ru", "de"
        };

        /// <summary>
        /// A reference point that i could get on google maps.
        /// Since we know where on the texture that latitude/longitude corresponds to, we can easily place other locations that way.
        /// </summary>
        public static readonly MapLocation referencePoint = new MapLocation(3763, 9379, 48.44859869654213f, -5.141549387018938f);
    }
}