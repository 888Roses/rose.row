using UnityEngine;

namespace rose.row.console.commands
{
    public class LockSwordCommand : AbstractConsoleCommand
    {
        public override string root => "item.lock.sword";
        public override string description => "Locks the special hidden Sword item.";

        public override void execute()
        {
            PlayerPrefs.DeleteKey("sword unlock");
            PlayerPrefs.Save();
        }
    }
}
