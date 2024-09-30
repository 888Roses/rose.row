using rose.row.data;
using rose.row.easy_events;
using System;
using UnityEngine;

namespace rose.row.actor.health
{
    /// <summary>
    /// Reponsible for determining the new health of actors, as well as everything related to
    /// damage and so on. This calculates the different "health floor" and regeneration/damage
    /// based off of it.
    /// </summary>
    [RequireComponent(typeof(Actor))]
    public class CustomActorHealth : MonoBehaviour
    {
        #region initialization events

        // Adds this custom actor health script to any actor that was created in the world.

        private static bool _hasSubscribedToInitializationEvents = false;

        public static void subscribeToInitializationEvents()
        {
            if (_hasSubscribedToInitializationEvents)
                return;

            Events.onActorAwake.after += onActorAwakeEvent;
            Events.onActorHurt.after += onActorHurtEvent;

            _hasSubscribedToInitializationEvents = true;
        }

        private static void onActorAwakeEvent(Actor actor)
            => actor.transform.gameObject.AddComponent<CustomActorHealth>();

        #endregion initialization events

        #region global events

        private static Action<Actor, DamageInfo> _onActorHurt;

        private static void onActorHurtEvent(Actor actor, DamageInfo info)
            => _onActorHurt?.Invoke(actor, info);

        private void subscribeEvents()
        {
            _onActorHurt += checkActorHurtEventValidity;
        }

        private void unsubscribeEvents()
        {
            _onActorHurt -= checkActorHurtEventValidity;
        }

        private void checkActorHurtEventValidity(Actor actor, DamageInfo damageInfo)
        {
            if (actor != _actor)
                return;

            onActorHurt(damageInfo);
        }

        #endregion global events

        #region constants

        public static readonly ConstantHolder<float> healthPerHealthBar = new ConstantHolder<float>(
            name: "actor.health.health_per_bar",
            description: "The amount of health that one health bar represents.",
            defaultValue: 5f
        );

        public static readonly ConstantHolder<float> defaultActorHealth = new ConstantHolder<float>(
            name: "actor.health.default_health",
            description: "The default amount of health that an actor spawns with.",
            defaultValue: 100f
        );

        public static readonly ConstantHolder<float> lowestHealthFloorThreshold = new ConstantHolder<float>(
            name: "actor.health.floor_thresholds.lowest",
            description: "The maximum health needed to be counted as in the lowest health floor (or section).",
            defaultValue: 30f
        );

        public static readonly ConstantHolder<float> middleHealthFloorThreshold = new ConstantHolder<float>(
            name: "actor.health.floor_thresholds.middle",
            description: "The maximum health needed to be counted as in the middle health floor (or section).",
            defaultValue: 60f
        );

        public static readonly ConstantHolder<float> regenerationStartDelay = new ConstantHolder<float>(
            name: "actor.health.regeneration_start_delay",
            description: "The amount of seconds one needs to wait before being able to regenerate one's health up to the maximum of one's current health floor.",
            defaultValue: 11f
        );

        public static readonly ConstantHolder<float> regeneratedHealthPerSecond = new ConstantHolder<float>(
            name: "actor.health.regenerated_health_per_second",
            description: "The amount of health regenerated every second \"actor.health.regeneration_start_delay\" seconds after being shot at.",
            defaultValue: 5f
        );

        #endregion constants

        #region fields

        private Actor _actor;

        private float _lastTimeHurt = 0f;
        public float lastTimeHurt => _lastTimeHurt;

        #endregion fields

        #region unity

        private void Awake()
        {
            _actor = GetComponent<Actor>();
            subscribeEvents();
        }

        private void OnDestroy()
        {
            unsubscribeEvents();
        }

        private void Update()
        {
            updateRegeneration();
        }

        #endregion unity

        #region behaviour

        #region utility fields

        public float health => _actor.health;
        public int healthBars => getHealthBarsFromFloatHealth(health);
        public HealthFloor healthFloor => getHealthFloorFromFloatFloatHealth(health);
        public float healthFloorMaxHealth => getMaxHealthForHealthFloor(healthFloor);

        #endregion utility fields

        #region health regeneration

        /// <summary>
        /// Whether this actor can regenerate health or not.
        /// </summary>
        /// <returns>
        /// True if:
        /// <list type="bullet">
        /// <item>The <see cref="k_RegenerationStartDelay"/> seconds cooldown after being hit is reached.</item>
        /// <item>The actor's health is smaller than it's <see cref="healthFloorMaxHealth"/>.</item>
        /// </list>
        /// False otherwise.
        /// </returns>
        public bool canRegenerateHealth()
        {
            var regenerationCooldownElapsed = Time.time >= _lastTimeHurt + regenerationStartDelay.get();
            // Whether the player can regenerate or not based on it's current health relative to it's
            // current healh floor's maximum health.
            var canRegenerateRelativeHealth = health < healthFloorMaxHealth;

            return regenerationCooldownElapsed && canRegenerateRelativeHealth;
        }

        private void onActorHurt(DamageInfo _)
        {
            _lastTimeHurt = Time.time;
        }

        /// <summary>
        /// Checks if the actor can regenerate health or not. If it's the case, regenerate the
        /// health by the given <see cref="k_RegeneratedHealthPerSecond"/> amount every second.
        /// </summary>
        private void updateRegeneration()
        {
            if (canRegenerateHealth())
            {
                var healingRate = regeneratedHealthPerSecond.get() * Time.deltaTime;
                _actor.SetHealth(Mathf.Min(healthFloorMaxHealth, health + healingRate));
            }
        }

        #endregion health regeneration

        #endregion behaviour

        #region utility methods

        /// <summary>
        /// Calculates the count of health bars to be displayed for the given float health.
        /// </summary>
        /// <param name="health">
        /// The health value as a floating point number.
        /// </param>
        /// <returns>
        /// An Integer number representing the count of bars to be displayed given a float
        /// health value and a <see cref="k_HealthPerHealthBar"/>.
        /// </returns>
        public static int getHealthBarsFromFloatHealth(float health)
        {
            return Mathf.RoundToInt(health / healthPerHealthBar.get());
        }

        /// <summary>
        /// Whether the given health corresponds to the lowest health floor (from 0 to
        /// <see cref="k_LowestHealthFloorThreshold"/>) or not.
        /// </summary>
        /// <param name="health">
        /// The health value that you want to check.
        /// </param>
        /// <returns>
        /// True if it is in this health floor. False otherwise.
        /// </returns>
        public static bool isInLowestHealthFloor(float health)
        {
            return health <= lowestHealthFloorThreshold.get();
        }

        /// <summary>
        /// Whether the given health corresponds to the middle health floor (from
        /// <see cref="k_LowestHealthFloorThreshold"/> to <see cref="k_MiddleHealthFloorThreshold"/>)
        /// or not.
        /// </summary>
        /// <param name="health">
        /// The health value that you want to check.
        /// </param>
        /// <returns>
        /// True if it is in this health floor. False otherwise.
        /// </returns>
        public static bool isInMiddleHealthFloor(float health)
        {
            return health > lowestHealthFloorThreshold.get() && health <= middleHealthFloorThreshold.get();
        }

        /// <summary>
        /// Whether the given health corresponds to the highest health floor (from
        /// <see cref="k_MiddleHealthFloorThreshold"/> to more than that) or not.
        /// </summary>
        /// <param name="health">
        /// The health value that you want to check.
        /// </param>
        /// <returns>
        /// True if it is in this health floor. False otherwise.
        /// </returns>
        public static bool isInHighestHealthFloor(float health)
        {
            return health > middleHealthFloorThreshold.get();
        }

        /// <summary>
        /// Returns the current health portion within which this health value belongs.
        /// </summary>
        /// <param name="health">
        /// The health value that you want to check.
        /// </param>
        /// <returns>
        /// A <see cref="HealthFloor"/> corresponding to the current portion of health that
        /// the given value belongs in. For more information on those portions, see
        /// <see cref="HealthFloor.Highest"/>, <see cref="HealthFloor.Middle"/>, and
        /// <see cref="HealthFloor.Lowest"/>.
        /// </returns>
        public static HealthFloor getHealthFloorFromFloatFloatHealth(float health)
        {
            if (isInLowestHealthFloor(health))
                return HealthFloor.Lowest;

            if (isInMiddleHealthFloor(health))
                return HealthFloor.Middle;

            return HealthFloor.Highest;
        }

        /// <summary>
        /// Returns the maximum health for a given <see cref="HealthFloor"/>.
        /// For instance, that means that the actor will not be able to regenerate past this number.
        /// </summary>
        /// <param name="floor">
        /// The health floor whose max health you wish to acquire.
        /// </param>
        /// <returns>
        /// A Float number representing the maximum health of that given <see cref="HealthFloor"/>.
        /// </returns>
        public static float getMaxHealthForHealthFloor(HealthFloor floor)
        {
            switch (floor)
            {
                case HealthFloor.Lowest:
                    return lowestHealthFloorThreshold.get();

                case HealthFloor.Middle:
                    return middleHealthFloorThreshold.get();

                case HealthFloor.Highest:
                    return 100f;

                default:
                    return 100f;
            }
        }

        #endregion utility methods
    }
}