using rose.row.data;
using rose.row.weapons;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerPickupWeapons : PlayerBehaviour
    {
        public static readonly ConstantHolder<float> pickupDistance = new ConstantHolder<float>(
            name: "actor.player.pickup_distance",
            description: "Maximum distance of the player from a dropped item to be able to pick it up.",
            defaultValue: 5f
        );

        public static PickupableWeapon currentlyWatchedPickupableWeapon;

        private void checkPickup(Vector3 position, RaycastHit hit)
        {
            //var distance = Vector3.Distance(position.with(y: 0), hit.point.with(y: 0));
            //if (distance <= pickupDistance.get())
            //    checkWeaponsInProximity(hit);
        }

        private void checkWeaponsInProximity()
        {
            foreach (var weapon in PickupableWeapons.activePickupableWeapons)
            {
                if (weapon == null)
                    continue;

                checkWeapon(transform.position, weapon);
            }
        }

        private void checkWeapon(Vector3 position, PickupableWeapon weapon)
        {
            var bounds = weapon.bounds;
            if (bounds.Contains(position) || Vector3.Distance(position, bounds.ClosestPoint(position)) <= 0.25f)
            {
                currentlyWatchedPickupableWeapon = weapon;

                if (SteelInput.GetInput(SteelInput.KeyBinds.Use).GetButtonDown())
                    PickupableWeapons.equipDroppedWeapon(FpsActorController.instance.actor, weapon);
            }
        }

        private void updatePickupDetection()
        {
            currentlyWatchedPickupableWeapon = null;

            //var ray = player.cameraForward();
            //if (Physics.Raycast(ray, out var hit, 10f, 1 << 0))
            //    checkPickup(ray.origin, hit);
            checkWeaponsInProximity();
        }

        private void Update()
        {
            updatePickupDetection();
        }
    }
}
