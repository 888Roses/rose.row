using rose.row.default_package;
using rose.row.util;
using UnityEngine;

namespace rose.row.weapons
{
    public class PlayerRecoilParent : Singleton<PlayerRecoilParent>
    {
        public FpsActorController controller;

        public Transform positionRecoilParent;
        public Transform rotationRecoilParent;

        private void Start()
        {
            if (controller == null)
            {
                Debug.LogError($"Cannot start PlayerRecoilParent because the FPSActorController is null!");
                return;
            }

            if (controller.weaponParent == null)
            {
                Debug.LogError($"Cannot start PlayerRecoilParent because the weapon parent of the FPSActorController is null!");
                return;
            }

            rotationRecoilParent = new GameObject("Rotation").transform;
            rotationRecoilParent.SetParent(transform);
            rotationRecoilParent.resetLocalTransform();

            positionRecoilParent = new GameObject("Position").transform;
            positionRecoilParent.SetParent(rotationRecoilParent);
            positionRecoilParent.resetLocalTransform();

            transform.localPosition = Vector3.zero;

            controller.weaponParent.SetParent(positionRecoilParent);
            controller.weaponParent.resetLocalTransform();

            Debug.Log($"Initialized weapon recoil parent.");
        }

        private void Update()
        {
            const float k_Speed = 10f;

            rotationRecoilParent.localRotation = Quaternion.Slerp(rotationRecoilParent.localRotation, Quaternion.identity, Time.deltaTime * k_Speed);
            positionRecoilParent.localPosition = Vector3.Lerp(positionRecoilParent.localPosition, Vector3.zero, Time.deltaTime * k_Speed);
        }

        public void applyRecoil(Vector3 position, Quaternion rotation)
        {
            rotationRecoilParent.localRotation = rotation;
            positionRecoilParent.localPosition = position;
        }
    }
}
