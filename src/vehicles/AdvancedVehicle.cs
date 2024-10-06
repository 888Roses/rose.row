using HarmonyLib;
using rose.row.dev.dev_editor;
using rose.row.util;
using System.Text;
using UnityEngine;

namespace rose.row.vehicles
{
    public class AdvancedVehicle : MonoBehaviour
    {
        #region vehicle

        private Vehicle _vehicle;
        public Vehicle vehicle => _vehicle;

        public void setVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        #endregion

        #region debug

        private void OnGUI()
        {
            if (DevMainInfo.showVehicleInfo)
            {
                var camera = PlayerFpParent.instance.fpCamera;
                var screenPosition = camera.WorldToScreenPoint(_vehicle.GetPosition());
                screenPosition.y = Screen.height - screenPosition.y;
                var size = new Vector2(400, 500);
                var finalPosition = (Vector2) screenPosition;
                var rect = new Rect(finalPosition, size);

                var content = new StringBuilder();

                content.AppendLine($"Vehicle: {_vehicle.name}");
                foreach (var seat in _vehicle.seats)
                {
                    var occupant = seat.IsOccupied()
                        ? seat.occupant.getNameSafe()
                        : "Empty";
                    content.AppendLine($"\"{seat.name}\": {occupant}");
                }

                GUI.Label(rect, content.ToString());
            }
        }

        #endregion
    }

    [HarmonyPatch(typeof(Vehicle), "Start")]
    internal class AdvancedVehiclePatcher
    {
        [HarmonyPostfix]
        static void postfix(Vehicle __instance)
        {
            __instance.gameObject.AddComponent<AdvancedVehicle>()
                                 .setVehicle(__instance);
        }
    }
}
