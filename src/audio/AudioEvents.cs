using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.match;
using rose.row.spawn;

namespace rose.row.audio
{
    public static class AudioEvents
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onSpawnPointCaptured.after += onPointCaptured;
        }

        private static void onPointCaptured(SpawnPoint point, int team, bool isInitialTeam)
        {
            if (isInitialTeam)
                return;

            var faction = CurrentMatch.getFactionOfTeam(team);

            if (faction == null)
            {
                if (point.tryGetAdvancedSpawnPoint(out var advancedSpawnPoint))
                {
                    if (advancedSpawnPoint.previousOwner == CurrentMatch.playerTeam)
                    {
                        Audio.play(AudioRegistry.capturePointNeutralizedEnemy.get());
                    }
                    else
                    {
                        Audio.play(AudioRegistry.capturePointNeutralizedFriendly.get());
                    }
                }
                else
                {
                    // No advanced spawn point was found, so we play the default sound.
                    Audio.play(AudioRegistry.capturePointNeutralizedFriendly.get());
                }
            }
            else
            {
                Audio.play(faction.captureJingle.get());
            }
        }
    }
}
