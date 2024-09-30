using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.match;

namespace rose.row.audio
{
    public static class AudioEvents
    {
        public static void initializeEvents()
        {
            Events.onPointCaptured.after += onPointCaptured;
        }

        private static void onPointCaptured(SpawnPoint point, int team, bool isInitialTeam)
        {
            if (isInitialTeam)
                return;

            var faction = CurrentMatch.getFactionOfTeam(team);

            if (faction == null)
            {
                Audio.play(AudioRegistry.capturePointNeutralized.get());
            }
            else
            {
                Audio.play(faction.captureJingle.get());
            }
        }
    }
}
