using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.actors
{
    [HarmonyPatch(typeof(Actor), "Awake")]
    internal class OnActorAwakePatcher
    {
        [HarmonyPrefix]
        private static void prefix(Actor __instance) => Events.onActorAwake.before?.Invoke(__instance);

        [HarmonyPostfix]
        private static void postfix(Actor __instance) => Events.onActorAwake.after?.Invoke(__instance);
    }
}