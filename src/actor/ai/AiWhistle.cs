using rose.row.data;
using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.ai
{
    public class AiWhistle : AiBehaviour
    {
        public static readonly ConstantHolder<float> whistleCooldown = new ConstantHolder<float>(
            name: "actor.ai.whistle_cooldown",
            description: "Time before an AI can whistle again.",
            defaultValue: 8f
        );

        public static readonly ConstantHolder<float> whistleDelayMin = new ConstantHolder<float>(
            name: "actor.ai.whistle_delay_min",
            description: "Minimum time between hearing someone whistle and whistling too.",
            defaultValue: 0.5f
        );

        public static readonly ConstantHolder<float> whistleDelayMax = new ConstantHolder<float>(
            name: "actor.ai.whistle_delay_max",
            description: "Maximum time between hearing someone whistle and whistling too.",
            defaultValue: 2f
        );

        public static readonly ConstantHolder<float> whistleSpontaneousProbability = new ConstantHolder<float>(
            name: "actor.ai.whistle_spontaneous_probability",
            description: "Chance that an AI actor will whistle out of nowhere.",
            defaultValue: 0.00001f
        );

        private float _whistleCooldown;

        public void tryWhistle(string comingFrom)
        {
            Invoke("doWhistle", Random.Range(whistleDelayMin.get(), whistleDelayMin.get()));
        }

        private void doWhistle()
        {
            if (Time.time > _whistleCooldown)
            {
                Events.onWhistle.before?.Invoke(ai.controller);
                Events.onAiWhistle.before?.Invoke(ai);

                _whistleCooldown = Time.time + whistleCooldown.get();

                Audio.playAtPoint(AudioRegistry.whistles.random().get(), transform.position);

                Events.onWhistle.after?.Invoke(ai.controller);
                Events.onAiWhistle.after?.Invoke(ai);
            }
        }

        // Using fixed update so it's not frame dependant.
        private void FixedUpdate()
        {
            if (Random.value <= whistleSpontaneousProbability.get() && !ai.controller.dead())
            {
                tryWhistle("actor.behaviour.spontaneous");
            }
        }
    }
}
