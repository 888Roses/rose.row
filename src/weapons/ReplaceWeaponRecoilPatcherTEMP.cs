using HarmonyLib;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.ApplyRecoil))]
    internal class ReplaceWeaponRecoilPatcherTEMP
    {
        [HarmonyPrefix]
        static bool prefix(FpsActorController __instance, Vector3 impulse)
        {
            var force = Mathf.Clamp(impulse.magnitude, 0, 2f) / 2;
            PlayerRecoilParent.instance.applyRecoil(new Vector3(0, 0.02f, -0.15f) * force, Quaternion.Euler(new Vector3(-3f, 0, 0) * force));

            return false;
        }
    }
}
