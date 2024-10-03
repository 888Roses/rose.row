using UnityEngine;

namespace rose.row.ui
{
    public static class ScreenOrder
    {
        public static readonly int mainMenuUi = 20;

        public static readonly int ingame = 1;
        public static int displayablesScreen => ingame + 0;
        public static int killfeedMenu => ingame + 1;
        public static int crosshair => ingame + 1;
        public static int weaponPickupPopup => ingame + 1;
        public static int deathScreen => ingame + 2;
        public static int spawnMenu => ingame + 3;
        public static int weaponSelectionMenu => ingame + 4;
        public static int vehicleSelectionMenu => ingame + 4;
        public static int endGameScreen => ingame + 5;
        public static int leaderboard => ingame + 6;
        public static int inGameMenu => ingame + 7;

        public static int console => Mathf.Max(mainMenuUi, ingame) + 100;
    }
}