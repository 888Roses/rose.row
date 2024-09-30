using HarmonyLib;
using rose.row.util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static InstantActionMaps;

namespace rose.row.match
{
    public static class Maps
    {
        public static List<MapEntry> cachedMaps;

        public static void cacheMaps()
        {
            cachedMaps = new List<MapEntry>();

            foreach (ModInformation modInformation in ModManager.instance.GetActiveMods())
            {
                if (modInformation.HasLoadedContent())
                {
                    foreach (FileInfo fileInfo in modInformation.content.GetMaps())
                    {
                        cacheMap(modInformation, fileInfo);
                    }
                }
            }
        }

        private static void cacheMap(ModInformation modInformation, FileInfo mapFile)
        {
            MapEntry mapEntry = new MapEntry
            {
                sceneName = mapFile.FullName,
                isCustomMap = true
            };

            Traverse
                .Create(mapEntry)
                .Field("sourceMod")
                .SetValue(modInformation);

            Traverse
                .Create(mapEntry)
                .Method("LoadOrGenerateMetaData", Path.GetFileNameWithoutExtension(mapFile.Name))
                .GetValue();

            cachedMaps.Add(mapEntry);
        }

        public static MapEntry getMapByName(string name)
        {
            if (cachedMaps == null)
                cacheMaps();

            return cachedMaps.FirstOrDefault(x =>
                x.getDisplayName().ToLowerInvariant() == name
                || x.sceneName.ToLowerInvariant() == name
            );
        }
    }
}