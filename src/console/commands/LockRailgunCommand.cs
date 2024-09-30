using UnityEngine;

namespace rose.row.console.commands
{
    public class LockRailgunCommand : AbstractConsoleCommand
    {
        public override string root => "item.lock.railgun";
        public override string description => "Locks the special hidden Railgun item.";

        public override void execute()
        {
            PlayerPrefs.DeleteKey("railgun unlock");
            PlayerPrefs.Save();
        }
    }
}
