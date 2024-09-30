using System.Collections.Generic;
using System.Linq;

namespace rose.row.util
{
    public static class MapUtil
    {
        public static IEnumerable<CustomMapEntry> customMapEntries() =>
            CustomMapsBrowser.instance.contentPanel.gameObject.GetComponentsInChildren<CustomMapEntry>();

        public static IEnumerable<InstantActionMaps.MapEntry> allMapEntries()
        {
            if (InstantActionMaps.instance == null)
                MainMenu.instance.OpenPageIndex(MainMenu.PAGE_INSTANT_ACTION);

            var entries = InstantActionMaps.instance.officialEntries;

            var customEntries = customMapEntries();
            if (customEntries.Count() == 0)
                return entries;

            entries.AddRange(customEntries.Select(x => x.entry));
            return entries;
        }
    }
}