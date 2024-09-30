using HarmonyLib;

namespace rose.row.ui.ingame.screens.pause_menu
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.TogglePauseMenuInput))]
    internal class DisableDefaultPauseMenuPatcher
    {
        [HarmonyPrefix]
        private static bool prefix(ref bool __result)
        {
            __result = false;
            return false;
        }
    }
}