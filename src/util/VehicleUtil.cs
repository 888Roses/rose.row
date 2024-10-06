using rose.row.data;
using UnityEngine;

namespace rose.row.util
{
    public static class VehicleUtil
    {
        /// <summary>
        /// Retrieves the type of the vehicle from it's spawner.
        /// </summary>
        /// <param name="vehicle">The vehicle whose type you wish to retrieve</param>
        /// <returns>A <see cref="VehicleSpawner.VehicleSpawnType"/> representing the type of the provided vehicle.</returns>
        public static VehicleSpawner.VehicleSpawnType spawnType(this Vehicle vehicle)
        {
            return vehicle.spawner.typeToSpawn;
        }

        /// <summary>
        /// Gets the icon associated to this type of vehicle.
        /// </summary>
        /// <param name="spawnType">The type of vehicle whose icon you wish to retrieve.</param>
        /// <returns>A Texture2D representing the icon of that vehicle <see cref="VehicleSpawner.VehicleSpawnType"/>.</returns>
        public static Texture2D icon(this VehicleSpawner.VehicleSpawnType spawnType)
        {
            var vehicleIcon = ImageRegistry.resourceNeutral.get();
            switch (spawnType)
            {
                case VehicleSpawner.VehicleSpawnType.Jeep:
                    vehicleIcon = ImageRegistry.resourceTransportVehicle.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.JeepMachineGun:
                    vehicleIcon = ImageRegistry.resourceTransportVehicle.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.Quad:
                    vehicleIcon = ImageRegistry.resourceMotorcycle.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.Tank:
                    vehicleIcon = ImageRegistry.resourceHeavyArmor.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.Apc:
                    vehicleIcon = ImageRegistry.resourceArmoredCar.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.AttackPlane:
                    vehicleIcon = ImageRegistry.resourceFighterPlane.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.BomberPlane:
                    vehicleIcon = ImageRegistry.resourceHeavyFighterPlane.get();
                    break;
            };

            return vehicleIcon;
        }

        public static Seat getEmptyOrAiSeat(this Vehicle vehicle, bool allowDriverSeat)
        {
            for (int i = ((!allowDriverSeat) ? 1 : 0); i < vehicle.seats.Count; i++)
            {
                if (!vehicle.seats[i].IsOccupied() || vehicle.seats[i].occupant.aiControlled)
                    return vehicle.seats[i];
            }

            return null;
        }
    }
}
