using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.loadmodworker
{
    [HarmonyPatch(typeof(ModManager), "ReloadModContent")]
    internal class ReloadModContentPatcher
    {
        [HarmonyPrefix]
        static void prefix() => Events.onStartLoadingMods.before?.Invoke();

        [HarmonyPostfix]
        static void postfix() => Events.onStartLoadingMods.after?.Invoke();
    }
}
