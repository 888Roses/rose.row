using HarmonyLib;

namespace rose.row.ui.ingame.ingame_displayables.displayables
{
    [HarmonyPatch(typeof(ResupplyCrate), "Start")]
    internal class ResupplyCrateDisplayablePatch
    {
        [HarmonyPrefix]
        private static void prefix(ResupplyCrate __instance)
            => __instance.gameObject.AddComponent<ResupplyCrateDisplayable>();
    }
}