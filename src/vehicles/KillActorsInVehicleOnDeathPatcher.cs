using HarmonyLib;

namespace rose.row.vehicles
{
    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.OnVehicleDisabled))]
    internal class KillActorsInVehicleOnDeathOnVehicleDisabledPatcher
    {
        [HarmonyPrefix]
        static void prefix(Vehicle __instance, DamageInfo info)
        {
            foreach (var possibleActor in ActorManager.instance.actors)
            {
                if (possibleActor != null
                    && !possibleActor.dead
                    && possibleActor.seat != null
                    && possibleActor.seat.vehicle == __instance)
                {
                    possibleActor.Kill(info);
                    continue;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.Die))]
    internal class KillActorsInVehicleOnDeathDiePatcher
    {
        [HarmonyPrefix]
        static void prefix(Vehicle __instance, DamageInfo info)
        {
            foreach (var possibleActor in ActorManager.instance.actors)
            {
                if (possibleActor != null
                    && !possibleActor.dead
                    && possibleActor.seat != null
                    && possibleActor.seat.vehicle == __instance)
                {
                    possibleActor.Kill(info);
                    continue;
                }
            }
        }
    }
}
