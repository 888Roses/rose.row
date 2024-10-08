using rose.row.vehicles;
using System.Collections.Generic;
using System.Linq;

namespace rose.row.util
{
    public static class ModManagerUtil
    {
        public static List<VehicleInfo> getVehicles(this ModManager manager)
        {
            var vehicleList = new List<VehicleInfo>();

            #region vehicles

            for (int i = 0; i < ActorManager.instance.defaultVehiclePrefabs.Length; i++)
                vehicleList.Add(new(ActorManager.instance.defaultVehiclePrefabs[i], new((VehicleSpawner.VehicleSpawnType) i)));

            manager.vehiclePrefabs.ToList().ForEach(k =>
            {
                foreach (var prefab in k.Value)
                    vehicleList.Add(new(prefab, new(k.Key)));
            });

            //foreach (var vehicleSpawnType in VehicleSpawner.ALL_VEHICLE_TYPES)
            //{
            //    // Default vehicle prefabs.
            //    vehicleList.Add(new VehicleInfo(
            //        prefab: ActorManager.instance.defaultVehiclePrefabs[(int) vehicleSpawnType],
            //        type: new CompoundSpawnType(vehicleSpawnType)
            //    ));

            //    // Custom vehicle prefabs.
            //    foreach (var vehicleGameObject in manager.vehiclePrefabs[vehicleSpawnType])
            //    {
            //        vehicleList.Add(new VehicleInfo(
            //            prefab: vehicleGameObject,
            //            type: new CompoundSpawnType(vehicleSpawnType)
            //        ));
            //    }
            //}

            #endregion

            #region turrets

            for (int i = 0; i < ActorManager.instance.defaultTurretPrefabs.Length; i++)
                vehicleList.Add(new(ActorManager.instance.defaultTurretPrefabs[i], new((TurretSpawner.TurretSpawnType) i)));

            manager.turretPrefabs.ToList().ForEach(k =>
            {
                foreach (var prefab in k.Value)
                    vehicleList.Add(new(prefab, new(k.Key)));
            });

            //foreach (var turretSpawnType in TurretSpawner.ALL_TURRET_TYPES)
            //{
            //    vehicleList.Add(new VehicleInfo(
            //        prefab: ActorManager.instance.defaultTurretPrefabs[(int) turretSpawnType],
            //        type: new CompoundSpawnType(turretSpawnType)
            //    ));

            //    foreach (var turretGameObject in manager.turretPrefabs[turretSpawnType])
            //    {
            //        vehicleList.Add(new VehicleInfo(
            //            prefab: turretGameObject,
            //            type: new CompoundSpawnType(turretSpawnType)
            //        ));
            //    }
            //}

            #endregion

            return vehicleList;
        }
    }
}
