using System.Linq;

namespace rose.row.spawn
{
    public static class Spawn
    {
        /// <summary>
        /// Represents a vehicle in which the player desires to spawn.
        /// </summary>
        public static Vehicle vehicleToSpawnIn;

        /// <summary>
        /// Removes the <see cref="vehicleToSpawnIn"/>, so that we no longer specify that
        /// we wish to spawn in a vehicle next time.
        /// </summary>
        public static void clearVehicleToSpawnIn()
        {
            vehicleToSpawnIn = null;
        }

        public static void spawn(Vehicle vehicle)
        {
            vehicleToSpawnIn = vehicle;

            // If the player hasn't selected a spawn point yet, we want to set their selected spawn point to the first non null
            // spawn point, of the same team as the player.
            if (MinimapUi.instance.selectedSpawnPoint == null)
            {
                var point = ActorManager.instance.spawnPoints.FirstOrDefault(
                    x => x != null
                      && x.owner == LocalPlayer.actor.team
                      && x != MinimapUi.instance.selectedSpawnPoint
                );

                MinimapUi.SelectSpawnPoint(point);
            }

            LoadoutUi.instance.OnDeployClick();
        }
    }
}
