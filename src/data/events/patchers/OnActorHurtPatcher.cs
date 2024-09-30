using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.Damage))]
    internal class OnActorHurtPatcher
    {
        [HarmonyPrefix]
        private static void prefix(Actor __instance, DamageInfo info)
            => Events.onActorHurt.before?.Invoke(__instance, info);

        [HarmonyPostfix]
        private static void postfix(Actor __instance, DamageInfo info)
            => Events.onActorHurt.after?.Invoke(__instance, info);
    }
}