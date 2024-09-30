using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.modmanager
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.OnAllContentLoaded))]
    internal class OnAllContentModsLoadedPatcher
    {
        [HarmonyPrefix]
        static void prefix() => Events.onAllContentLoaded.before?.Invoke();

        [HarmonyPostfix]
        static void postfix() => Events.onAllContentLoaded.after?.Invoke();
    }
}
