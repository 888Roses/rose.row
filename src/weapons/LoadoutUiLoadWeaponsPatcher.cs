using HarmonyLib;
using rose.row.util;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(LoadoutUi), "LoadSlotEntry")]
    internal class LoadoutUiLoadWeaponsPatcher
    {
        [HarmonyPrefix]
        static bool prefix(LoadoutUi __instance, ref WeaponManager.WeaponEntry __result, WeaponManager.WeaponSlot entrySlot, string keyName)
        {
            if (!PlayerPrefs.HasKey(keyName))
            {
                foreach (WeaponManager.WeaponEntry weaponEntry in WeaponManager.instance.allWeapons)
                {
                    if (LoadoutUiUtil.canUseWeaponEntry(weaponEntry) && weaponEntry.slot == entrySlot)
                    {
                        __result = weaponEntry;
                        return false;
                    }
                }

                __result = WeaponManager.instance.allWeapons[0];
                return false;
            }

            int @int = PlayerPrefs.GetInt(keyName);
            if (@int == -1)
            {
                __result = WeaponManager.instance.allWeapons[0];
                return false;
            }

            WeaponManager.WeaponEntry weaponEntry2 = null;
            foreach (WeaponManager.WeaponEntry weaponEntry3 in WeaponManager.instance.allWeapons)
            {
                if (LoadoutUiUtil.canUseWeaponEntry(weaponEntry3) && (weaponEntry3.slot == entrySlot || (entrySlot == WeaponManager.WeaponSlot.LargeGear && weaponEntry3.slot == WeaponManager.WeaponSlot.Gear)))
                {
                    if (weaponEntry3.nameHash == @int)
                    {
                        __result = weaponEntry3;
                        return false;
                    }
                    if (weaponEntry2 == null)
                    {
                        weaponEntry2 = weaponEntry3;
                    }
                }
            }

            __result = weaponEntry2 == null ? WeaponManager.instance.allWeapons[0] : weaponEntry2;
            return false;
        }
    }
}
