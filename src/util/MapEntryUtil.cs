using HarmonyLib;
using static InstantActionMaps;

namespace rose.row.util
{
    public static class MapEntryUtil
    {
        public static Traverse getMetaData(this MapEntry entry)
        {
            return Traverse.Create(entry).Field("metaData");
        }

        public static int getSuggestedBots(this MapEntry entry)
        {
            return (int) entry.getMetaData().Field("suggestedBots").GetValue();
        }

        public static string getDisplayName(this MapEntry entry)
        {
            return (string) entry.getMetaData().Field("displayName").GetValue();
        }
    }
}