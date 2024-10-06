using rose.row.dev.dev_editor;
using UnityEngine;

namespace rose.row.console.commands
{
    public class ShowVehicleInfoCommand : AbstractConsoleCommand
    {
        public override string root => "debug.vehicle_info";
        public override string description => "Enables or disables displayed vehicle info.";

        public override void execute()
        {
            DevMainInfo.showVehicleInfo = !DevMainInfo.showVehicleInfo;

            if (DevMainInfo.showVehicleInfo)
                Debug.Log("Enabled vehicle info.");
            else
                Debug.Log("Disabled vehicle info.");
        }
    }
}
