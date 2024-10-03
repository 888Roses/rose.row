using rose.row.actor;
using rose.row.data;
using rose.row.util;
using UnityEngine;

namespace rose.row.weapons
{
    /// <summary>
    /// A weapon that can live in-game and be picked up by an actor.
    /// </summary>
    public class PickupableWeapon : MonoBehaviour
    {
        /// <summary>
        /// How long can a dropped weapon stay in the world before disappearing.
        /// </summary>
        /// <remarks>
        /// If a weapon's <see cref="overridenDroppedWeaponLifetime"/> is set to any value
        /// greater than 0, then <see cref="overridenDroppedWeaponLifetime"/> will be used instead of this.
        /// </remarks>
        public static readonly ConstantHolder<float> k_DroppedWeaponLifetime = new ConstantHolder<float>(
            name: "weapon.dropped.lifetime",
            description: "How long can a dropped weapon stay in the world before disappearing.",
            defaultValue: 10f
        );

        /// <summary>
        /// The weapon attached to this pickupable weapon.
        /// </summary>
        /// <remarks>
        /// Note: This script is ALWAYS disabled as long as the weapon is still a pickable.
        /// </remarks>
        public Weapon weapon;
        /// <summary>
        /// The entry associated to this pickupable weapon.
        /// </summary>
        /// <remarks>
        /// Note: Use this instead of <see cref="Weapon.weaponEntry"/> of <see cref="weapon"/> since it'd returns null.
        /// </remarks>
        public WeaponManager.WeaponEntry entry;

        /// <summary>
        /// How long can this dropped weapon stay in the world before being destroyed.
        /// When set to 0, the default <see cref="k_DroppedWeaponLifetime"/> constant will be used.
        /// Otherwise, this value will be used.
        /// </summary>
        public float overridenDroppedWeaponLifetime = 0.0f;
        /// <summary>
        /// The time when the dropped weapon was created in the world.
        /// This is used to calculate the current lifetime of the dropped weapon.
        /// </summary>
        public float birthTime;

        /// <summary>
        /// How much time has this weapon stayed in the world already.
        /// </summary>
        public float getCurrentLifetime()
        {
            return Time.time - birthTime;
        }

        /// <summary>
        /// Gets the maximum time a weapon can stay in the world before disappearing.
        /// This takes into account both the <see cref="overridenDroppedWeaponLifetime"/> and <see cref="k_DroppedWeaponLifetime"/>.
        /// </summary>
        public float getMaxLifetime()
        {
            return overridenDroppedWeaponLifetime > 0.0f ? overridenDroppedWeaponLifetime : k_DroppedWeaponLifetime.get();
        }

        private void Awake()
        {
            birthTime = Time.time;
        }

        private void Update()
        {
            if (getCurrentLifetime() > getMaxLifetime())
            {
                dispose();
            }

            if (IngameDebugGizmos.IsEnabled())
            {
                bounds.gizmoDrawEdges(Color.green, 1f);
            }
        }

        /// <summary>
        /// Destroys this weapon and removes it from the <see cref="PickupableWeapons.activePickupableWeapons"/> list.
        /// </summary>
        public void dispose()
        {
            PickupableWeapons.activePickupableWeapons.removeIfContained(this);
            Destroy(gameObject);
        }

        private MeshRenderer[] _renderers;

        /// <summary>
        /// The bounds englobing this element.
        /// </summary>
        public Bounds bounds
        {
            get
            {
                if (_renderers == null)
                    _renderers = weapon.GetComponentsInChildren<MeshRenderer>();

                var bounds = _renderers[0].bounds;
                // We don't want to encapsulate or do any math on the first renderer's bounds since
                // this is already what's being used as a base bounding box.
                var isFirstRenderer = true;

                foreach (var renderer in _renderers)
                {
                    if (isFirstRenderer)
                    {
                        isFirstRenderer = false;
                        continue;
                    }

                    if (renderer.transform.parent == weapon.transform
                        && renderer.bounds.extents.magnitude >= bounds.extents.magnitude / 2)
                    {
                        bounds.Encapsulate(renderer.bounds);
                    }
                }

                // for (var i = 1; i < renderers.Length; i++)
                // {
                //     // Huge mistake here with the '=' instead of '=='. Wondering how that's gonna affect the gameplay.
                //     if (renderers[i].transform.parent = weapon.transform)
                //     {
                //         if (renderers[i].bounds.extents.magnitude >= bounds.extents.magnitude / 2)
                //         {
                //             bounds.Encapsulate(renderers[i].bounds);
                //         }
                //     }
                // }

                return bounds;
            }
        }
    }
}
