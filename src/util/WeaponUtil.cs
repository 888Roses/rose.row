using HarmonyLib;
using rose.row.weapon;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.util
{
    public static class WeaponUtil
    {
        public static readonly Dictionary<Weapon, AdvancedWeapon> registry = new Dictionary<Weapon, AdvancedWeapon>();

        public static AdvancedWeapon getAdvancedWeapon(this Weapon weapon)
        {
            if (registry.ContainsKey(weapon))
                return registry[weapon];

            var advancedWeapon = weapon.gameObject.AddComponent<AdvancedWeapon>();
            advancedWeapon.weapon = weapon;
            registry.Add(weapon, advancedWeapon);
            return advancedWeapon;
        }

        public static float getAimingAmount(this Weapon weapon)
            => weapon.getAdvancedWeapon().aimAmount;

        public static bool canUseEyeAsMuzzle(this Weapon weapon)
            => (bool) Traverse.Create(weapon).Method("CanUseEyeAsMuzzle").GetValue();

        public static Projectile spawnProjectile(this Weapon weapon, Vector3 direction, Vector3 muzzlePosition, bool hasUser)
            => (Projectile) Traverse.Create(weapon).Method("SpawnProjectile", direction, muzzlePosition, hasUser).GetValue();
    }
}
