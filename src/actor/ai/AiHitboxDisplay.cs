using rose.row.dev.dev_editor;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.ai
{
    public class AiHitboxDisplay : AiBehaviour
    {
        private void FixedUpdate()
        {
            if (!DevMainInfo.isDebugEnabled || !DevMainInfo.showHitboxes)
                return;

            if (Vector3.Distance(transform.position, LocalPlayer.actor.transform.position) > 25f)
                return;

            var colour = ai.controller.actor.isEnemy() ? Color.red : Color.cyan;

            if (DevMainInfo.showHitboxes)
            {
                ai.controller.actor.skinnedRenderer.bounds.gizmoDrawEdges(colour, Time.fixedDeltaTime * 2);
            }

            if (DevMainInfo.showBones)
            {
                var bones = ai.controller.actor.animatedBones;
                for (int i = 0; i < bones.Length; i++)
                {
                    if (i > 0)
                    {
                        var previous = bones[i - 1];
                        var current = bones[i];

                        if (current.parent != previous)
                            continue;

                        IngameDebugGizmos.DrawLine(
                            start: previous.transform.position,
                            end: current.transform.position,
                            color: colour,
                            duration: Time.fixedDeltaTime * 2
                        );
                    }
                }
            }
        }
    }
}
