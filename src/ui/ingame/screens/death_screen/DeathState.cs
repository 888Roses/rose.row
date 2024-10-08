using rose.row.default_package;
using rose.row.easy_events;
using rose.row.ui.cursor;
using rose.row.util;
using UnityEngine;

namespace rose.row.ui.ingame.screens.death_screen
{
    public class DeathState : Singleton<DeathState>
    {
        #region enabled state

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;

            if (enabled)
            {
                FpsActorController.instance.FirstPersonCamera();
                LoadoutUi.Hide(true);
                KillCamera.Hide();
            }
        }

        #endregion

        #region events

        public static void subscribeToInitializationEvents()
        {
            MouseCursor.cursorHandlers.Add((e) => instance != null && instance.isEnabled);
            Events.onPlayerSpawn.after += onPlayerSpawn;
            Events.onPlayerDie.after += onPlayerDie;

            Events.onLoadoutUiShow.after += onLoadoutUiShow;
        }

        private static void onPlayerDie(FpsActorController controller)
            => instance.setEnabled(true);

        private static void onPlayerSpawn(FpsActorController controller)
            => instance.setEnabled(false);

        private static void onLoadoutUiShow(bool showLoadout)
        {
            var loadoutUi = LoadoutUi.instance;
            loadoutUi.SetLoadoutVisible(false);
            loadoutUi.SetMinimapVisible(false);
            loadoutUi.hideCanvas();
        }

        #endregion

        #region gameObject management

        public static void create()
        {
            var gameObject = new GameObject("Death State");
            gameObject.AddComponent<DeathState>();
        }

        public void dispose()
        {
            Destroy(gameObject);
        }

        #endregion

        private void Start()
        {
            setEnabled(true);
        }
    }
}
