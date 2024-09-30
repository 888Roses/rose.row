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

    [HarmonyPatch(typeof(LoadoutUi), nameof(LoadoutUi.Show))]
    internal class LoadoutUiShowPatch
    {
        [HarmonyPrefix]
        private static void prefix(bool tactics) => Events.onLoadoutUiShow.before?.Invoke(tactics);

        [HarmonyPostfix]
        private static void postfix(bool tactics) => Events.onLoadoutUiShow.after?.Invoke(tactics);
    }
}