using HarmonyLib;

namespace rose.row.ui.ingame.screens.death_screen
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.SpawnAt))]
    internal class SpawnPlayerInDesiredVehiclePatcher
    {
        [HarmonyPostfix]
        private static void postfix(FpsActorController __instance)
        {
            if (DeathScreen.canEnterQueuedVehicle())
            {
                var vehicle = DeathScreen.queuedVehicle;
                if (vehicle.AllSeatsTaken())
                {
                    if (vehicle.seats.Count > 1)
                    {
                        vehicle.seats[1].occupant.LeaveSeat(false);
                    }
                    else
                    {
                        vehicle.seats[0].occupant.LeaveSeat(false);
                    }
                }

                __instance.actor.EnterVehicle(vehicle);
            }

            return;
        }
    }
}