using rose.row.data;
using UnityEngine;

namespace rose.row.dev.dev_editor
{
    public static class DevMainInfo
    {
        public static bool isDebugEnabled;

        public static bool isInSandboxLevel;
        public static bool isGameplayFrozen;
        public static bool isCursorLocked;
        public static bool forceAim;
        public static bool isFlying;
        public static bool showVehicleInfo;
        public static bool showHitboxes;
        public static bool showBones;
        public static bool showPickupableWeaponBoxes;

        public static readonly ConstantHolder<float> debugDistance = new(
            name: "debug.distance",
            description: "Maximum visible distance for gizmos.",
            defaultValue: 50f
        );

        public static bool isGizmoRenderable(Vector3 gizmoPosition)
        {
            return Vector3.Distance(LocalPlayer.actor.Position(), gizmoPosition) <= debugDistance.get();
        }
    }
}
