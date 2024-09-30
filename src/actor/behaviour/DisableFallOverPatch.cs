using HarmonyLib;
using rose.row.data;
using rose.row.easy_events;

namespace rose.row.actor.behaviour
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.FallOver))]
    internal class DisableFallOverPatch
    {
        [HarmonyPrefix]
        private static bool prefix(Actor __instance)
        {
            Events.onActorFallOver.before?.Invoke(__instance);
            return (bool) Constants.defaultValues[Constants.k_ActorCanFallOver];
        }

        [HarmonyPostfix]
        private static void postfix(Actor __instance) => Events.onActorFallOver.after?.Invoke(__instance);
    }
}