using rose.row.data.factions;
using rose.row.match;
using UnityEngine;

namespace rose.row.util
{
    public static class SpawnPointUtil
    {
        public static void randomizeSpawnPoints()
        {
            var points = ActorManager.instance.spawnPoints;
            var r = new System.Random();
            for (int n = points.Length - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                var temp = points[n];
                points[n] = points[k];
                points[k] = temp;
            }

            var teamSwitcher = 0;
            foreach (var spawnPoint in points)
            {
                spawnPoint.SetOwner(teamSwitcher, true);

                if (r.NextDouble() < 0.25f)
                {
                    spawnPoint.gameObject.SetActive(false);
                    continue;
                }

                // 25% balance randomization.
                if (r.NextDouble() < 0.75f)
                    teamSwitcher++;

                // TODO: Change this to allow for a third team in the same game.
                if (teamSwitcher >= 2)
                {
                    teamSwitcher = -1;
                }
            }

            OrderManager.RefreshAllOrders();
            MinimapUi.UpdateSpawnPointButtons();
        }

        public static bool isAroundSpawnPoint(this Vector3 position, SpawnPoint spawnPoint, float additionalRange = 0)
        {
            var distance = Vector3.Distance(
                a: position,
                b: spawnPoint.transform.position
            );

            if (distance <= spawnPoint.GetCaptureRange() + additionalRange)
                return true;

            return false;
        }

        public static bool isAroundSpawnPoint(this Transform transform,
                                              SpawnPoint spawnPoint,
                                              float additionalRange = 0)
            => isAroundSpawnPoint(transform.position, spawnPoint, additionalRange);

        public static bool isAroundSpawnPoint(this Actor actor,
                                              SpawnPoint spawnPoint,
                                              float additionalRange = 0)
            => isAroundSpawnPoint(actor.transform.position, spawnPoint, additionalRange);

        public static bool isAroundSpawnPoint(this ActorController controller,
                                              SpawnPoint spawnPoint,
                                              float additionalRange = 0)
            => isAroundSpawnPoint(controller.actor, spawnPoint, additionalRange);

        public static bool isOwnedBy(this SpawnPoint spawnPoint, int team)
            => spawnPoint.owner == team;

        public static bool isOwnedBy(this SpawnPoint spawnPoint, Actor actor)
            => spawnPoint.isOwnedBy(actor.team);

        public static bool isOwnedBy(this SpawnPoint spawnPoint, ActorController controller)
            => spawnPoint.isOwnedBy(controller.team());

        public static AbstractFaction owningFaction(this SpawnPoint spawnPoint)
        {
            if (spawnPoint.owner == -1)
                return null;

            if (spawnPoint.isOwnedBy(CurrentMatch.playerTeam))
                return Factions.playerFaction;

            return CurrentMatch.enemyFaction;
        }
    }
}