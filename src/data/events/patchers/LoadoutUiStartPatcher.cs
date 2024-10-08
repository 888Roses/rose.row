using HarmonyLib;
using rose.row.easy_events;

namespace rose.row
{
    [HarmonyPatch(typeof(LoadoutUi), "Start")]
    internal class LoadoutUiStartPatch
    {
        [HarmonyPrefix]
        private static void prefix() => Events.onLoadoutUiStart.before?.Invoke();

        [HarmonyPostfix]
        private static void postfix() => Events.onLoadoutUiStart.after?.Invoke();
    }
}