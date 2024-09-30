using HarmonyLib;
using rose.row.data;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.main
{
    [HarmonyPatch(typeof(GameModeBase), "SpawnDeadActorsOfTeam")]
    internal class ChangeRespawnTimePatcher
    {
        public static ConstantHolder<float> maxSpawnTime = new ConstantHolder<float>(
            name: "game.spawn.max_spawn_time",
            description: "The maximum time after which you may spawn when clicking the spawn button.",
            defaultValue: 2f
        );

        [HarmonyPrefix]
        private static bool prefix(GameModeBase __instance, int team, bool ignoreDeadTime = true)
        {
            List<Actor> list = new List<Actor>();

            foreach (Actor actor in ActorManager.instance.actors)
            {
                var isSameTeam = actor.team == team;
                var isReadyToSpawn = ignoreDeadTime || actor.deathTimestamp + maxSpawnTime.get() < Time.time;

                if (isSameTeam && actor.IsReadyToSpawn() && isReadyToSpawn)
                    list.Add(actor);
            }

            __instance.StartCoroutine(__instance.SpawnActorListDeferred(list, team));
            return false;
        }
    }
}