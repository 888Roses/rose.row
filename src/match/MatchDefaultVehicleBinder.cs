using rose.row.data.factions;
using System;
using System.Linq;
using UnityEngine;
using static VehicleSpawner;

namespace rose.row.match
{
    /// <summary>
    /// Responsible for determining what prefab goes into what type of vehicle spawner.
    /// </summary>
    public static class MatchDefaultVehicleBinder
    {
        public static GameObject[] cachedVehicles;

        public static GameObject getVehicleByName(string name)
        {
            return cachedVehicles.FirstOrDefault(x => x.name.ToLowerInvariant().Trim() == name);
        }

        public static GameObject getPrefabForVehicleType(VehicleSpawnType type, AbstractFaction faction)
        {
            switch (type)
            {
                case VehicleSpawnType.Jeep:
                    return getVehicleByName("jeep");

                case VehicleSpawnType.JeepMachineGun:
                    return getVehicleByName("jeep mg");

                case VehicleSpawnType.Apc:
                    return getVehicleByName("tank");

                case VehicleSpawnType.AttackHelicopter:
                    return getVehicleByName("jeep");

                case VehicleSpawnType.TransportHelicopter:
                    return getVehicleByName("jeep mg");

                case VehicleSpawnType.Quad:
                    return getVehicleByName("new quadbike");

                case VehicleSpawnType.Tank:
                    return getVehicleByName("tank");
            }

            return null;
        }

        public static void updateVehicles()
        {
            if (cachedVehicles == null)
                cachedVehicles = GameManager.instance.gameInfo.team[0].AvailableVehiclePrefabs().ToArray();

            Debug.Log("Available vehicles:");
            foreach (var vehicle in cachedVehicles)
                if (vehicle != null)
                    Debug.Log($" * '{vehicle.name}'");

            // TODO: Only allows for 2 teams. Allow for 3 if we wanna have three factions.
            for (int i = 0; i < 2; i++)
            {
                var faction = i == CurrentMatch.playerTeam ? Factions.playerFaction : CurrentMatch.enemyFaction;

                for (int j = 0; j < Enum.GetValues(typeof(VehicleSpawnType)).Length; j++)
                {
                    var type = (VehicleSpawnType) j;
                    setVehicle(i, type, getPrefabForVehicleType(type, faction));
                }
            }
        }

        public static void setVehicle(int team, VehicleSpawnType type, GameObject prefab)
        {
            GameManager.instance.gameInfo.team[team].SetVehicle(type, prefab);
        }
    }
}