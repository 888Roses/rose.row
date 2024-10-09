using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.util;

namespace rose.row.actor.player
{
    public static class PlayerFpsSoundManager
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorHurt.after += onActorHurt;
            Events.onActorDie.after += onActorDie;
        }

        private static void onActorDie(Actor actor, DamageInfo info, bool silentKill)
        {
            if (actor.aiControlled)
                return;

            Audio.play(AudioRegistry.die.random().get());
        }

        private static void onActorHurt(Actor actor, DamageInfo info)
        {
            if (actor.aiControlled)
                return;

            Audio.play(AudioRegistry.fpsPain.random().get());
        }
    }
}
