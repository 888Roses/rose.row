using HarmonyLib;
using rose.row.spawn;

namespace rose.row.ui.ingame.screens.death_screen
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.SpawnAt))]
    internal class SpawnPlayerInDesiredVehiclePatcher
    {
        [HarmonyPostfix]
        private static void postfix(FpsActorController __instance)
        {
            var vehicle = Spawn.vehicleToSpawnIn;

            if (vehicle == null)
                return;

            Spawn.clearVehicleToSpawnIn();

            if (vehicle.dead)
                return;

            Seat targetSeat = null;

            if (vehicle.AllSeatsTaken())
            {
                foreach (var seat in vehicle.seats)
                {
                    if (!seat.occupant.aiControlled || seat.IsDriverSeat())
                        continue;

                    // TODO: Watch out for flying actors!
                    seat.occupant.LeaveSeat(true);
                    targetSeat = seat;
                    break;
                }

                if (targetSeat == null)
                    return;
            }
            else
            {
                targetSeat = vehicle.GetEmptySeat(false);
            }

            if (targetSeat == null)
                return;

            __instance.actor.EnterSeat(targetSeat, true);
        }
    }
}