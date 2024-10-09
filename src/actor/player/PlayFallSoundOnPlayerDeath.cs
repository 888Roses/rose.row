using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.util;

namespace rose.row.actor.player
{
    public static class PlayFallSoundOnPlayerDeath
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorDie.after += onActorDie;
        }

        private static void onActorDie(Actor actor, DamageInfo info, bool isSilentKill)
        {
            if (actor != null && !actor.aiControlled && info.type == DamageInfo.DamageSourceType.FallDamage)
            {
                Audio.play(AudioRegistry.die.random().get());
                Audio.play(AudioRegistry.dieFallBody.random().get());
            }
        }
    }
}
