using rose.row.vehicles;
using System.Collections.Generic;

namespace rose.row.util
{
    public static class ModManagerUtil
    {
        public static List<VehicleInfo> getVehicles(this ModManager manager)
        {
            var vehicleList = new List<VehicleInfo>();

            #region vehicles

            foreach (var vehicleSpawnType in VehicleSpawner.ALL_VEHICLE_TYPES)
            {
                vehicleList.Add(new VehicleInfo(
                    prefab: ActorManager.instance.defaultVehiclePrefabs[(int) vehicleSpawnType],
                    type: new CompoundSpawnType(vehicleSpawnType)
                ));

                foreach (var vehicleGameObject in manager.vehiclePrefabs[vehicleSpawnType])
                {
                    vehicleList.Add(new VehicleInfo(
                        prefab: vehicleGameObject,
                        type: new CompoundSpawnType(vehicleSpawnType)
                    ));
                }
            }

            #endregion

            #region turrets

            foreach (var turretSpawnType in TurretSpawner.ALL_TURRET_TYPES)
            {
                vehicleList.Add(new VehicleInfo(
                    prefab: ActorManager.instance.defaultTurretPrefabs[(int) turretSpawnType],
                    type: new CompoundSpawnType(turretSpawnType)
                ));

                foreach (var turretGameObject in manager.turretPrefabs[turretSpawnType])
                {
                    vehicleList.Add(new VehicleInfo(
                        prefab: turretGameObject,
                        type: new CompoundSpawnType(turretSpawnType)
                    ));
                }
            }

            #endregion

            return vehicleList;
        }
    }
}
