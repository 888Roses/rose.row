using rose.row.data.factions;
using System.Linq;
using UnityEngine;

namespace rose.row.match
{
    public static class Factions
    {
        public static int currentFactionIndex = 0;
        public static AbstractFaction playerFaction => getFromIndex(currentFactionIndex);

        public static AbstractFaction getFromIndex(int index)
        {
            if (registeredFactions.Length == 0)
                return null;

            index = Mathf.Clamp(index, 0, registeredFactions.Length - 1);
            return registeredFactions[index];
        }

        public static AbstractFaction getFromISO2(string iso2)
        {
            Debug.Log("Registered factions count: " + registeredFactions.Count());
            iso2 = iso2.ToLowerInvariant();
            if (iso2 == "gb" || iso2 == "fr")
                return faction<UsFaction>();

            return registeredFactions.FirstOrDefault(x => x.factionISO2.ToLowerInvariant() == iso2);
        }

        public static readonly AbstractFaction[] registeredFactions = new AbstractFaction[]
        {
            new GmFaction(),
            new UsFaction(),
            new RuFaction(),
        };

        public static AbstractFaction faction<T>() where T : AbstractFaction
        {
            return registeredFactions.FirstOrDefault(x => x is T);
        }
    }
}