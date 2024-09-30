using UnityEngine;

namespace rose.row.vehicles
{
    /// <summary>
    /// Represents a spawn type containing both a vehicle and a turret spawn type.
    /// </summary>
    public class CompoundSpawnType
    {
        public enum Type
        {
            Vehicle, Turret
        }

        public Type type;
        public VehicleSpawner.VehicleSpawnType vehicleSpawnType;
        public TurretSpawner.TurretSpawnType turretSpawnType;

        public CompoundSpawnType(VehicleSpawner.VehicleSpawnType vehicleSpawnType)
        {
            this.vehicleSpawnType = vehicleSpawnType;
            turretSpawnType = default;

            type = Type.Vehicle;
        }

        public CompoundSpawnType(TurretSpawner.TurretSpawnType turretSpawnType)
        {
            this.turretSpawnType = turretSpawnType;
            vehicleSpawnType = default;

            type = Type.Turret;
        }
    }

    /// <summary>
    /// Represents a vehicle and information about it.
    /// </summary>
    public readonly struct VehicleInfo
    {
        public readonly GameObject prefab;
        public readonly CompoundSpawnType type;

        public VehicleInfo(GameObject prefab, CompoundSpawnType type)
        {
            this.prefab = prefab;
            this.type = type;
        }
    }
}
