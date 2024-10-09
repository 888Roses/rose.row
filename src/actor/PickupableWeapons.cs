using HarmonyLib;
using rose.row.easy_events;
using rose.row.weapons;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.actor
{
    /// <summary>
    /// Responsible for managing everything that has to do with dropping weapons, as well as managing actors' picked up weapons.
    /// </summary>
    public static class PickupableWeapons
    {
        public const int k_PickedUpWeaponSlotIndex = 4;
        public static readonly List<PickupableWeapon> activePickupableWeapons = new List<PickupableWeapon>();

        private static readonly Dictionary<Actor, int> _lastEquipedActiveWeaponSlot = new Dictionary<Actor, int>();

        public static void subscribeToInitializationEvents()
        {
            Events.onActorSwitchActiveWeapon.before += onBeforeActorSwitchActiveWeapon;
            Events.onActorSwitchActiveWeapon.after += onAfterActorSwitchActiveWeapon;
        }

        private static void onBeforeActorSwitchActiveWeapon(Actor actor, int slot)
        {
            var previousSlot = actor.activeWeapon == null ? 0 : actor.activeWeapon.slot;

            if (_lastEquipedActiveWeaponSlot.ContainsKey(actor))
                _lastEquipedActiveWeaponSlot[actor] = previousSlot;
            else
                _lastEquipedActiveWeaponSlot.Add(actor, previousSlot);
        }

        private static void onAfterActorSwitchActiveWeapon(Actor actor, int slot)
        {
            if (!_lastEquipedActiveWeaponSlot.ContainsKey(actor))
            {
                Debug.LogWarning($"Tried to drop an active weapon but the actor wasn't registered in the dictionary.");
                return;
            }

            var lastSlot = _lastEquipedActiveWeaponSlot[actor];
            if (lastSlot == k_PickedUpWeaponSlotIndex && slot != k_PickedUpWeaponSlotIndex)
                actor.dropWeaponOnGround(k_PickedUpWeaponSlotIndex);
        }

        /// <summary>
        /// Equips a dropped weapon for a given actor.
        /// </summary>
        /// <param name="actor">The actor that wishes to pick up the dropped weapon.</param>
        /// <param name="pickupableWeapon">The weapon dropped on the floor.</param>
        public static void equipDroppedWeapon(Actor actor, PickupableWeapon pickupableWeapon)
        {
            if (actor == null || pickupableWeapon == null)
            {
                return;
            }

            PickupableWeapon currentlyPickedupWeapon = null;
            if (actor.isHoldingPickedUpWeapon())
                currentlyPickedupWeapon = actor.dropWeaponOnGround(false);

            actor.EquipNewWeaponEntry(pickupableWeapon.entry, k_PickedUpWeaponSlotIndex, true);
            Traverse.Create(IngameUI.instance).Method("OnPlayerChangedWeapon").GetValue();
            activePickupableWeapons.Remove(pickupableWeapon);
            GameObject.Destroy(pickupableWeapon.gameObject);

            if (currentlyPickedupWeapon != null)
                activePickupableWeapons.Add(currentlyPickedupWeapon);
        }

        /// <summary>
        /// Drops a given weapon on the floor to be picked up by an entity.
        /// </summary>
        /// <param name="originalWeapon">The weapon you wish to drop on the floor.</param>
        public static PickupableWeapon dropWeaponOnGround(Weapon originalWeapon, bool autoRegister = true)
        {
            // * Create copy.
            // * Bring and rotate according to the floor under it.
            // * Add PickupableWeapon script that will manage everything related to it's dropped state.

            // Creates a copy of the weapon that we want to drop.
            var droppedWeapon = Object.Instantiate(
                original: originalWeapon,
                position: originalWeapon.transform.position,
                rotation: originalWeapon.transform.rotation);

            // Changing the layer of the instantiated weapon to 0 to make sure that it isn't still in the first person
            // layer that can be seen on top of everything else on the map.
            GameManager.SetupRecursiveLayer(droppedWeapon.transform, 0);

            // Disables the weapon so we can do all sort of raycasting without hitting the weapon's model by accident.
            droppedWeapon.gameObject.SetActive(false);

            // Grounds the weapon so it stays on the floor and doesn't float where the actor was standing before dying.
            if (Physics.Raycast(droppedWeapon.transform.position, Vector3.down, out RaycastHit hit, maxDistance: 100, layerMask: 1 << 0))
            {
                droppedWeapon.transform.position = hit.point;
                droppedWeapon.transform.up = hit.normal;
                droppedWeapon.transform.Translate(0, 0.1f, 0, Space.Self);
            }

            // Some spicy little random rotation in the mix so it's now always facing the same direction on the ground :)
            droppedWeapon.transform.Rotate(0, Random.Range(0, 360), 0, Space.World);

            var pickupableWeapon = droppedWeapon.gameObject.AddComponent<PickupableWeapon>();
            // Setup the dropped weapon.
            pickupableWeapon.weapon = droppedWeapon;
            pickupableWeapon.entry = originalWeapon.weaponEntry;
            // Register the dropped weapon.
            if (autoRegister)
                activePickupableWeapons.Add(pickupableWeapon);
            // Disables the weapon script so it cannot do anything anymore except live in the world.
            droppedWeapon.enabled = false;
            // We can now enable it again (disabled so that the grounded raycast doesn't hit anything contained by the weapon).
            droppedWeapon.gameObject.SetActive(true);
            // Removes the arms model.
            Object.Destroy(droppedWeapon.arms);

            return pickupableWeapon;
        }

        /// <summary>
        /// Drops the current active weapon of the actor on the ground.
        /// </summary>
        /// <param name="actor">The actor whose current active weapon you wish to drop.</param>
        public static PickupableWeapon dropWeaponOnGround(this Actor actor, bool autoRegister = true)
        {
            if (actor == null || actor.activeWeapon == null)
            {
                return null;
            }

            return dropWeaponOnGround(actor.activeWeapon, autoRegister);
            actor.DropWeapon(actor.activeWeapon.slot);
        }

        /// <summary>
        /// Drops the weapon at the given slot index of a given actor on the ground.
        /// </summary>
        /// <param name="actor">The actor whose weapon you wish to drop.</param>
        /// <param name="slot">In what slot is the weapon you want to drop.</param>
        public static void dropWeaponOnGround(this Actor actor, int slot)
        {
            if (actor == null || slot < 0 || slot >= actor.weapons.Length)
            {
                return;
            }

            dropWeaponOnGround(actor.weapons[slot]);
            actor.DropWeapon(slot);
        }

        /// <summary>
        /// Returns whether or not the actor is holding a weapon they picked up off of the ground or not.
        /// </summary>
        public static bool isHoldingPickedUpWeapon(this Actor actor)
        {
            return actor.activeWeapon != null && actor.activeWeapon.slot == k_PickedUpWeaponSlotIndex;
        }
    }
}
