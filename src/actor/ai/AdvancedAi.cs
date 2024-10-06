using rose.row.util;
using UnityEngine;

namespace rose.row.actor.ai
{
    public class AdvancedAi : MonoBehaviour
    {
        public AiActorController controller;
        public AiWhistle whistle;
        public AiHitboxDisplay hitboxDisplay;

        private void Awake()
        {
            whistle = use<AiWhistle>();
            hitboxDisplay = use<AiHitboxDisplay>();
        }

        public T use<T>() where T : AiBehaviour
        {
            var component = gameObject.AddComponent<T>();
            component.ai = this;
            return component;
        }

        public void forceFallOver()
        {
            if (controller.actor.IsSeated())
            {
                controller.actor.LeaveSeat(true);
            }
            if (controller.actor.IsOnLadder())
            {
                controller.actor.ExitLadder();
            }
            controller.actor.animator.SetBool(Actor.ANIM_PAR_RAGDOLLED, true);
            controller.actor.ragdoll.Ragdoll(controller.actor.cachedVelocity());
            controller.actor.controller.StartRagdoll();
            controller.actor.ik().weight = 0f;
            controller.actor.animator.SetLayerWeight(2, 1f);
            controller.actor.fallAction().Start();
            controller.actor.fallenOver = true;
            controller.actor.getupAction().Stop();
            controller.actor.setGetupActionWasStarted(false);
            if (controller.actor.HasUnholsteredWeapon())
            {
                controller.actor.activeWeapon.CancelReload();
                controller.actor.activeWeapon.SetAiming(false);
                controller.actor.activeWeapon.gameObject.SetActive(false);
            }
        }
    }
}
