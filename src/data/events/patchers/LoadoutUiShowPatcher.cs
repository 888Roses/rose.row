using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers
{
    [HarmonyPatch(typeof(LoadoutUi), nameof(LoadoutUi.Show))]
    internal class LoadoutUiShowPatcher
    {
        [HarmonyPrefix]
        private static void prefix(bool tactics) => Events.onLoadoutUiShow.before?.Invoke(tactics);

        [HarmonyPostfix]
        private static void postfix(bool tactics) => Events.onLoadoutUiShow.after?.Invoke(tactics);
    }
}