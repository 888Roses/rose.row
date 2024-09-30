using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.player
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.SpawnAt))]
    internal class OnPlayerSpawnPatch
    {
        [HarmonyPrefix]
        private static void prefix(FpsActorController __instance)
            => Events.onPlayerSpawn.before?.Invoke(__instance);

        [HarmonyPostfix]
        private static void postfix(FpsActorController __instance)
            => Events.onPlayerSpawn.after?.Invoke(__instance);
    }
}