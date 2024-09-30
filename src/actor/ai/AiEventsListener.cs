using rose.row.data;
using rose.row.easy_events;
using UnityEngine;

namespace rose.row.actor.ai
{
    public static class AiEventsListener
    {
        public static readonly ConstantHolder<float> whistleReactiveDistance = new ConstantHolder<float>(
            name: "actor.ai.whistle_listening_distance",
            description: "Range around a whistling actor where AI actors will have a chance of whistling too.",
            defaultValue: 10f
        );

        public static readonly ConstantHolder<float> whistleReactiveProbability = new ConstantHolder<float>(
            name: "actor.ai.whistle_listening_probability",
            description: "Probability that an AI actor will whistle too when hearing someone else whistling.",
            defaultValue: 0.15f
        );

        public static readonly ConstantHolder<float> whistleAfterKillProbability = new ConstantHolder<float>(
            name: "actor.ai.whistle_after_kill_probability",
            description: "Probability that an AI actor will whistle after killing someone.",
            defaultValue: 0.075f
        );

        public static readonly ConstantHolder<float> whistleAfterKillDistance = new ConstantHolder<float>(
            name: "actor.ai.whistle_after_kill_distance",
            description: "Distance from the kill that an AI actor must be to whistle after killing someone.",
            defaultValue: 5f
        );

        public static void initializeEvents()
        {
            Events.onWhistle.after += onWhistle;
            Events.onActorDie.after += onActorDie;
        }

        private static void onActorDie(Actor actor, DamageInfo info, bool isSilentKill)
        {
            whistleAfterDeath(actor, info, isSilentKill);
        }

        private static void whistleAfterDeath(Actor actor, DamageInfo info, bool isSilentKill)
        {
            if (isSilentKill)
                return;

            if (info.sourceActor != null && info.sourceActor != actor && !info.sourceActor.dead)
            {
                if (Random.value > whistleAfterKillProbability.get())
                    return;

                var dst = Vector3.Distance(actor.transform.position, info.sourceActor.transform.position);
                if (dst > whistleAfterKillDistance.get())
                    return;

                if (info.sourceActor.TryGetComponent(out AdvancedAi advancedAi))
                    advancedAi.whistle.tryWhistle();
            }
        }

        private static void onWhistle(ActorController controller)
        {
            var actorsAround = ActorManager.ActorsInRange(controller.transform.position, whistleReactiveDistance.get());
            foreach (var actor in actorsAround)
            {
                if (actor.controller == controller || !actor.aiControlled || actor.dead)
                    continue;

                if (Random.value <= whistleReactiveProbability.get())
                {
                    if (actor.TryGetComponent(out AdvancedAi advancedAi))
                    {
                        advancedAi.whistle.tryWhistle();
                    }
                }
            }
        }
    }
}
