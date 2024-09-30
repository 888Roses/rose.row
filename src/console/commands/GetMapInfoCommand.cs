using rose.row.util;
using UnityEngine;

namespace rose.row.console.commands
{
    public class GetMapInfoCommand : AbstractConsoleCommand
    {
        public override string root => "map.get_info";

        public override string description => "Throws in the console information about each and every loaded map.";

        public override void execute()
        {
            var maps = MapUtil.allMapEntries();
            if (maps == null)
            {
                Debug.LogError($"Could not find maps because the managers responsible for that are not initialised.");
                return;
            }

            foreach (var map in maps)
            {
                try
                {
                    Debug.Log($"Map '{map.getDisplayName()}':");
                    Debug.Log($"  Scene Name: '{map.sceneName}'");
                    Debug.Log($"  Suggested bot count: {map.getSuggestedBots()}");
                    Debug.Log($"  Night Version: {map.nightVersion}");
                }
                catch { }
            }
        }
    }
}