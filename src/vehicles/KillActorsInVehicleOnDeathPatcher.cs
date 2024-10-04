using HarmonyLib;

namespace rose.row.vehicles
{
    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.Die))]
    internal class KillActorsInVehicleOnDeathPatcher
    {
        [HarmonyPrefix]
        static void prefix(Vehicle __instance, DamageInfo info)
        {
            foreach (var seat in __instance.seats)
                if (seat.IsOccupied())
                    seat.occupant.Kill(info);
        }
    }
}
