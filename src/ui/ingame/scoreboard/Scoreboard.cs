using rose.row.client;
using rose.row.easy_events;
using rose.row.match;
using rose.row.util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.ui.ingame.scoreboard
{
    /// <summary>
    /// Responsible for managing the static side of the scoreboard, independant from the UI.
    /// </summary>
    public static class Scoreboard
    {
        public static Dictionary<Actor, PlayerInfo> players = new Dictionary<Actor, PlayerInfo>();

        public static void subscribeToInitializationEvents()
        {
            Events.onGameManagerStartLevel.after += onGameStarts;

            Events.onActorDie.after += onActorDies;
            Events.onPointCaptured.after += onPointCaptured;
        }

        private static void onPointCaptured(SpawnPoint point, int team, bool isInitialTeam)
        {
            if (isInitialTeam)
            {
                return;
            }

            if (point == null || ActorManager.instance == null)
            {
                if (point == null)
                    Debug.LogError($"Cannot activate scoreboard event 'onPointCaptured' for null spawn point.");

                if (ActorManager.instance == null)
                    Debug.LogError($"Cannot activate scoreboard event 'onPointCaptured' if actor manager is null.");

                return;
            }

            foreach (var actor in ActorManager.ActorsInRange(point.transform.position, point.GetCaptureRange()))
            {
                if (actor == null)
                {
                    Debug.LogWarning($"Cannot activate scoreboard event 'onPointCaptured' for null actor.");
                    continue;
                }

                if (players.ContainsKey(actor))
                {
                    players[actor].captures++;
                }
                else
                {
                    players.Add(actor, new PlayerInfo() { captures = 1 });
                }
            }
        }

        private static void onActorDies(Actor actor, DamageInfo info, bool silentKill)
        {
            if (actor == null)
            {
                Debug.LogError($"Cannot activate scoreboard event 'onActorDies' for null actor.");
                return;
            }

            if (players.ContainsKey(actor))
            {
                players[actor].deaths++;
            }
            else
            {
                players.Add(actor, new PlayerInfo() { deaths = 1 });
            }

            // TODO: Implement headshots alongside damage multiplier zones.
            if (info.sourceActor != null)
            {
                if (players.ContainsKey(info.sourceActor))
                {
                    players[info.sourceActor].kills++;
                }
                else
                {
                    players.Add(info.sourceActor, new PlayerInfo() { kills = 1 });
                }
            }
        }

        private static void onGameStarts()
        {
            players.Clear();

            if (ActorManager.instance == null)
            {
                Debug.LogError($"Can't initialize scoreboard one game start because actor manager is null.");
                return;
            }

            foreach (var actor in ActorManager.instance.actors)
            {
                if (actor == null)
                {
                    Debug.LogWarning($"Can't initialize scoreboard for null actor. Skipping.");
                    continue;
                }

                if (CurrentMatch.mission != null && CurrentMatch.enemyFaction == null)
                {
                    CurrentMatch.enemyFaction = CurrentMatch.mission.getRandomFaction();
                }

                var info = new PlayerInfo()
                {
                    name = actor.aiControlled ? actor.getNameSafe() : Client.name,

                    faction = (actor.team == CurrentMatch.playerTeam)
                            ? CurrentMatch.playerFaction
                            : CurrentMatch.enemyFaction,
                    rank = UnityEngine.Random.Range(0, 17)
                };

                players.Add(
                    key: actor,
                    value: info
                );

                try
                {
                    Debug.Log($"Initialized scoreboard for actor '{actor.getNameSafe()}'.");
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Could not generate actor scoreboard success message: {e.Message}");
                }
            }
        }
    }
}
