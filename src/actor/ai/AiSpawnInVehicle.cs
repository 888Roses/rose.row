using rose.row.easy_events;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.ai
{
    public static class AiSpawnInVehicle
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorSpawnAt.after += onActorSpawnAt;
        }

        private static bool isVehicleEnterable(Actor actor, Vehicle vehicle)
        {
            if (actor == null)
                return false;

            if (!vehicle.HasDriver() || vehicle.Driver().team != actor.team)
                return false;

            if (vehicle.GetEmptySeat(false) == null)
                return false;

            return true;
        }

        private static void onActorSpawnAt(Actor actor, Vector3 position, Quaternion rotation, WeaponManager.LoadoutSet forcedSet)
        {
            if (actor.isPlayer())
                return;

            foreach (var vehicle in ActorManager.instance.vehicles)
            {
                if (isVehicleEnterable(actor, vehicle))
                {
                    if (actor.EnterVehicle(vehicle))
                        break;
                }
            }
        }
    }
}
