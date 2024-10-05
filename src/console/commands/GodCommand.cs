using UnityEngine;

namespace rose.row.console.commands
{
    public class GodCommand : AbstractConsoleCommand
    {
        public static bool isGodModeOn = false;

        public override string root => "god";

        public override string description => "Turns on or off invinsibility.";

        public override void execute()
        {
            if (!GameManager.IsIngame() || FpsActorController.instance == null)
            {
                Debug.LogWarning($"Can only toggle god mode in a lobby.");
                return;
            }

            isGodModeOn = !isGodModeOn;
            FpsActorController.instance.actor.isInvulnerable = isGodModeOn;

            Debug.Log($"Turned {(isGodModeOn ? "on" : "off")} god mode.");
        }
    }
}
