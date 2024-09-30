using HarmonyLib;

namespace rose.row.util
{
    public static class LoadoutUiUtil
    {
        public static void showCustomized(this LoadoutUi loadout,
                                          bool tactics,
                                          bool showLoadout = true,
                                          bool showMinimap = true,
                                          bool showCanvas = true,
                                          bool showDeploymentTab = true,
                                          bool showTactics = true)
        {
            loadout.primaryWeaponSelection.Hide();
            loadout.secondaryWeaponSelection.Hide();
            loadout.gearWeaponSelection.Hide();

            if (showLoadout)
                loadout.SetLoadoutVisible(true);
            if (showMinimap)
                loadout.SetMinimapVisible(true);
            // loadout.targetSlot = -1;

            if (StrategyUi.IsOpen())
                StrategyUi.Hide();

            if (showCanvas)
                loadout.showCanvas();

            if (tactics && showTactics)
            {
                LoadoutUi.instance.ShowTacticsTab();
                return;
            }

            if (showDeploymentTab && showDeploymentTab)
                LoadoutUi.instance.ShowDeploymentTab();
        }

        public static void showCanvas(this LoadoutUi loadout) => Traverse.Create(loadout).Method("ShowCanvas").GetValue();

        public static void hideCanvas(this LoadoutUi loadout) => Traverse.Create(loadout).Method("HideCanvas").GetValue();
    }
}