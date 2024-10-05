using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.util;

namespace rose.row.actor.player
{
    public static class PlayCrushedSoundOnPlayerDeath
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorDie.after += onActorDie;
        }

        private static void onActorDie(Actor actor, DamageInfo info, bool isSilentKill)
        {
            if (actor != null && !actor.aiControlled && info.type == DamageInfo.DamageSourceType.VehicleRam)
            {
                Audio.play(AudioRegistry.dieCrushed.random().get());
            }
        }
    }
}
