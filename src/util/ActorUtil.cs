using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;

namespace rose.row.util
{
    public static class ActorUtil
    {
        public static string getNameSafe(this Actor actor)
        {
            if (actor.scoreboardEntry == null)
                return "";

            return actor.scoreboardEntry.nameText.text;
        }

        public static TimedAction hurtAction(this Actor actor)
        {
            return (TimedAction) Traverse.Create(actor).Field("hurtAction").GetValue();
        }

        public static Weapon equipWeapon(this Actor actor, Weapon weaponPrefab, int slot)
        {
            if (weaponPrefab == null)
                return null;

            var weapon = GameObject.Instantiate(weaponPrefab);
            weapon.enabled = true;

            if (actor.aiControlled)
                weapon.CullFpsObjects();

            var thirdPerson = weapon.thirdPersonTransform;

            if (thirdPerson != null)
            {
                if (!actor.aiControlled)
                {
                    var thirdPersonImposter = weapon.CreateTpImposter(out thirdPerson);
                    thirdPersonImposter.transform.parent = actor.controller.TpWeaponParent();
                    thirdPersonImposter.transform.localPosition = Vector3.zero;
                    thirdPersonImposter.transform.localRotation = Quaternion.identity;
                    thirdPersonImposter.transform.localScale = Vector3.one;

                    var imposterRenderers = thirdPersonImposter.GetComponentsInChildren<Renderer>();
                    var imposterColliders = thirdPersonImposter.GetComponentsInChildren<Collider>();

                    foreach (var imposterCollider in imposterColliders)
                        imposterCollider.enabled = false;

                    foreach (var imposterRenderer in imposterRenderers)
                        imposterRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;

                    thirdPersonImposter.SetActive(false);
                    actor.weaponImposterRenderers.Add(weapon, imposterRenderers);
                }

                thirdPerson.localEulerAngles = weapon.thirdPersonRotation;
                thirdPerson.localPosition = weapon.thirdPersonOffset;
                thirdPerson.localScale = new Vector3(weapon.thirdPersonScale, weapon.thirdPersonScale, weapon.thirdPersonScale);
            }

            if (!actor.aiControlled)
                weapon.SetupFirstPerson();

            weapon.FindRenderers(actor.aiControlled);
            weapon.Equip(actor);

            weapon.transform.parent = actor.controller.WeaponParent();
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.localScale = Vector3.one;

            weapon.slot = slot;
            weapon.ammo = weapon.configuration.ammo;
            actor.weapons[slot] = weapon;

            if (weapon.IsToggleable())
            {
                return weapon;
            }

            weapon.gameObject.SetActive(false);

            Debug.Log($"IM STILL STANDING LAAALAAALAAA");
            Debug.Log(weapon.weaponEntry);
            //Debug.Log(weapon.weaponEntry.type);
            //Debug.Log(actor);

            //switch (weapon.weaponEntry.type)
            //{
            //    case WeaponManager.WeaponEntry.LoadoutType.ResupplyAmmo:
            //        actor.hasAmmoBox = true;
            //        actor.ammoBoxSlot = slot;

            //        Debug.Log("1");
            //        break;

            //    case WeaponManager.WeaponEntry.LoadoutType.ResupplyHealth:
            //        actor.hasMedipack = true;
            //        actor.medipackSlot = slot;

            //        Debug.Log("2");
            //        break;

            //    case WeaponManager.WeaponEntry.LoadoutType.Repair:
            //        actor.hasRepairTool = true;
            //        actor.repairToolSlot = slot;

            //        Debug.Log("3");
            //        break;

            //    case WeaponManager.WeaponEntry.LoadoutType.SmokeScreen:
            //        actor.hasSmokeScreen = true;
            //        actor.smokeScreenSlot = slot;

            //        Debug.Log("4");
            //        break;
            //}

            Debug.Log($"IM NOT STANDING :((((");

            return weapon;
        }
    }
}