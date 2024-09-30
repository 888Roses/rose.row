using HarmonyLib;
using rose.row.easy_events;

namespace rose.row
{
    [HarmonyPatch(typeof(SpawnPoint), nameof(SpawnPoint.SetOwner))]
    internal class CapturePointSetOwnerPatcher
    {
        [HarmonyPrefix]
        private static void prefix(SpawnPoint __instance, int team, bool initialOwner)
            => Events.onPointCaptured.before?.Invoke(__instance, team, initialOwner);

        [HarmonyPostfix]
        private static void postfix(SpawnPoint __instance, int team, bool initialOwner)
            => Events.onPointCaptured.after?.Invoke(__instance, team, initialOwner);
    }
}