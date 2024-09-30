using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using UnityEngine;

namespace rose.row.ui.spawn_menu
{
    public class SpawnMenuScreen : Singleton<SpawnMenuScreen>
    {
        #region instance

        public static void create()
        {
            var gameObject = new GameObject("Spawn Menu");
            gameObject.AddComponent<SpawnMenuScreen>();
        }

        #endregion

        #region enable/disable

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        public void enable()
        {
            setEnabled(true);
        }

        public void disable()
        {
            setEnabled(false);
        }

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            updateEnabledState();
        }

        private void updateEnabledState()
        {
            _screen.gameObject.SetActive(_isEnabled);
        }

        #endregion

        private void Awake()
        {
            build();
        }

        #region building

        private UiScreen _screen;
        private SpawnMenuElement _element;

        private void build()
        {
            _screen = UiFactory.createUiScreen(
                name: "Spawn Menu",
                order: ScreenOrder.spawnMenu,
                parent: transform);

            _element = UiFactory.createUiElement<SpawnMenuElement>(
                name: "Menu",
                screen: _screen);
        }

        #endregion
    }
}
