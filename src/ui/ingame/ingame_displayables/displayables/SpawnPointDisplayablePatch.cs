using HarmonyLib;
using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables.displayables
{
    [HarmonyPatch(typeof(SpawnPoint), "Start")]
    internal class SpawnPointDisplayablePatch
    {
        [HarmonyPostfix]
        static void postfix(SpawnPoint __instance)
        {
            Debug.Log($"Registered spawn point {__instance} as a displayable holding spawn point.");
            __instance.gameObject.AddComponent<SpawnPointDisplayable>().setSpawnPoint(__instance);
        }
    }
}
