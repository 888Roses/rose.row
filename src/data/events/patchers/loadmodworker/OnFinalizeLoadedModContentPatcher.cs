using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.loadmodworker
{
    [HarmonyPatch(typeof(ModManager), "FinalizeLoadedModContent")]
    internal class OnFinalizeLoadedModContentPatcher
    {
        [HarmonyPrefix]
        static void prefix() => Events.onFinalizeLoadedModContent.before?.Invoke();

        [HarmonyPostfix]
        static void postfix() => Events.onFinalizeLoadedModContent.after?.Invoke();
    }
}
