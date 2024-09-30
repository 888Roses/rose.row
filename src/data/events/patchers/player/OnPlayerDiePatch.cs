using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.player
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.Die))]
    internal class OnPlayerDiePatch
    {
        [HarmonyPrefix]
        private static void prefix(FpsActorController __instance)
            => Events.onPlayerDie.before?.Invoke(__instance);

        [HarmonyPostfix]
        private static void postfix(FpsActorController __instance)
            => Events.onPlayerDie.after?.Invoke(__instance);
    }
}