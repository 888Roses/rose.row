using HarmonyLib;

namespace rose.row.actor.player
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.ToggleNightVision))]
    internal class DisableNightVisionPatchToggle
    {
        [HarmonyPrefix]
        static bool prefix() => false;
    }

    [HarmonyPatch(typeof(GameManager), nameof(GameManager.EnableNightVision))]
    internal class DisableNightVisionPatchEnable
    {
        [HarmonyPrefix]
        static bool prefix() => false;
    }

    [HarmonyPatch(typeof(GameManager), nameof(GameManager.DisableNightVision))]
    internal class DisableNightVisionPatchDisable
    {
        [HarmonyPrefix]
        static bool prefix() => false;
    }
}
