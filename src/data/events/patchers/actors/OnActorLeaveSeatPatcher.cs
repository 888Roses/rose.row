using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.actors
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.LeaveSeat))]
    internal class OnActorLeaveSeatPatcher
    {
        [HarmonyPrefix]
        static void prefix(Actor __instance, bool forcedByFallingOver) => Events.onActorLeaveSeat.before?.Invoke(__instance, forcedByFallingOver);

        [HarmonyPostfix]
        static void postfix(Actor __instance, bool forcedByFallingOver) => Events.onActorLeaveSeat.after?.Invoke(__instance, forcedByFallingOver);
    }
}
