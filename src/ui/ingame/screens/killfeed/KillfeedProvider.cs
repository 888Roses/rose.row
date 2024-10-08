using rose.row.data;
using rose.row.easy_events;
using rose.row.killfeed.info;
using rose.row.util;
using UnityEngine;

namespace rose.row.ui.ingame.screens.killfeed
{
    public static class KillfeedProvider
    {
        public static bool canExecute()
        {
            return KillfeedManager.instance != null;
        }

        public static void subscribeToInitializationEvents()
        {
            Events.onActorDie.after += onKilled;
            // Using the "before" event instead of the "after" event so that we get
            // the XP even if we killed the guy (or girl, or person) :)
            Events.onActorHurt.before += onActorHurt;

            Events.onSpawnPointCaptured.after += onPointCaptured;
        }

        private static void onPointCaptured(SpawnPoint spawnPoint, int team, bool isInitialTeam)
        {
            if (!canExecute())
                return;

            foreach (var actor in ActorManager.ActorsInRange(spawnPoint.transform.position, spawnPoint.GetCaptureRange()))
            {
                if (team == -1f)
                {
                    KillfeedManager.instance.registerItem(new CapturePointNeutralizedInfo(
                        spawnPoint: spawnPoint
                    ), actor);
                }

                else if (team == actor.team)
                {
                    KillfeedManager.instance.registerItem(new CapturePointCapturedInfo(
                        spawnPoint: spawnPoint
                    ), actor);
                }
            }
        }

        public static void createKillfeedManager()
        {
            var manager = new GameObject("Kill Feed Mananger");
            manager.AddComponent<KillfeedManager>();
        }

        public static bool isInvalid(Actor target, DamageInfo info)
        {
            return target == null || info.sourceActor == null;
        }

        private static void onActorHurt(Actor target, DamageInfo info)
        {
            if (!canExecute())
                return;

            if (isInvalid(target, info))
                return;

            if (target.dead)
                return;

            if (target == info.sourceActor)
                return;

            if (target.team == info.sourceActor.team)
                return;

            var source = info.sourceActor;

            KillfeedManager.instance.registerItem(new HurtInfo(
                source: source,
                killed: target,
                damageInfo: info,
                isSilentKill: false
            ), source);
        }

        public static void onKilled(Actor target, DamageInfo info, bool isSilentKill)
        {
            if (!canExecute())
                return;

            if (isInvalid(target, info))
                return;

            var source = info.sourceActor;

            if (target == info.sourceActor)
            {
                KillfeedManager.instance.registerItem(new SuicideInfo(
                    source: source,
                    killed: target,
                    damageInfo: info,
                    isSilentKill: isSilentKill
                ), source);

                return;
            }

            if (target.team == source.team)
            {
                KillfeedManager.instance.registerItem(new TeamKillInfo(
                    source: source,
                    killed: target,
                    damageInfo: info,
                    isSilentKill: isSilentKill
                ), source);
            }
            else
            {
                KillfeedManager.instance.registerItem(new KillInfo(
                    source: source,
                    killed: target,
                    damageInfo: info,
                    isSilentKill: isSilentKill
                ), source);

                // If the actor is in a point.
                var closestSpawnPointFromSource = ActorManager.ClosestSpawnPoint(source.transform.position);
                var isSourceInClosestSpawnPoint = source.isAroundSpawnPoint(
                    spawnPoint: closestSpawnPointFromSource,
                    additionalRange: Constants.k_SpawnPointDefenseAttackAdditionalRange
                );
                var isTargetInClosestSpawnPointFromSource = target.isAroundSpawnPoint(
                    spawnPoint: closestSpawnPointFromSource,
                    additionalRange: Constants.k_SpawnPointDefenseAttackAdditionalRange
                );
                if (isSourceInClosestSpawnPoint && isTargetInClosestSpawnPointFromSource)
                {
                    var isClosestSpawnPointOwnerBySource = closestSpawnPointFromSource.isOwnedBy(source);

                    // That means that the source defended their own point.
                    if (isClosestSpawnPointOwnerBySource)
                    {
                        KillfeedManager.instance.registerItem(new CapturePointDefenseInfo(
                            source: source,
                            killed: target,
                            damageInfo: info,
                            isSilentKill: isSilentKill
                        ), source);
                    }
                    // Otherwise, it's an attack kill.
                    else
                    {
                        KillfeedManager.instance.registerItem(new CapturePointAttackInfo(
                            source: source,
                            killed: target,
                            damageInfo: info,
                            isSilentKill: isSilentKill
                        ), source);
                    }
                }
            }
        }
    }
}