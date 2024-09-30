using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.vehicles
{
    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.Die))]
    internal class OnVehicleDiePatcher
    {
        [HarmonyPrefix]
        private static void prefix(Vehicle __instance, DamageInfo info)
            => Events.onVehicleDie.before?.Invoke(__instance, info);

        [HarmonyPostfix]
        private static void postfix(Vehicle __instance, DamageInfo info)
            => Events.onVehicleDie.after?.Invoke(__instance, info);
    }
}