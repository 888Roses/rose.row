using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.ReturnToMenu))]
    internal class OnReturnToMenuPatcher
    {
        [HarmonyPrefix]
        private static void prefix() => Events.onReturnToMenu.before?.Invoke();

        [HarmonyPostfix]
        private static void postfix() => Events.onReturnToMenu.after?.Invoke();
    }
}