﻿using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.behaviour
{
    public static class PlayPainSoundOnHurt
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorHurt.after += onActorHurt;
        }

        private static void onActorHurt(Actor actor, DamageInfo info)
        {
            if (actor == null)
            {
                Debug.LogError($"Cannot play pain sound for null actor.");
                return;
            }

            if (info.sourceActor == null || !actor.aiControlled)
            {
                return;
            }

            if (info.sourceActor.team == actor.team)
            {
                return;
            }

            Audio.playAtPoint(AudioRegistry.hitPain.random().get(), info.point);
        }
    }
}
