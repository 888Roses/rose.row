using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.ui;
using System;
using System.Linq;
using UnityEngine;

namespace rose.row.dev.dev_info_screen
{
    public class DeveloperInfoScreen : Singleton<DeveloperInfoScreen>
    {
        #region instance

        public static void registerAllDevMenus()
        {
            foreach (var type in AppDomain
                                        .CurrentDomain
                                        .GetAssemblies()
                                        .SelectMany(x => x.GetTypes()))
            {
                if (type.BaseType == null
                    || type.BaseType != typeof(AbstractDeveloperMenu))
                    continue;

                var menu = AbstractDeveloperMenu.create(type);
                instance.main.register(menu, menu.activateKey);
                Debug.Log($"Registered dev menu {type.Name}.");
            }
        }

        public static void create()
        {
            var gameObject = new GameObject("Dev Info");
            gameObject.AddComponent<DeveloperInfoScreen>();
            DontDestroyOnLoad(gameObject);

            registerAllDevMenus();
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
        private DeveloperInfoMainElement _element;
        public DeveloperInfoMainElement main => _element;

        private void build()
        {
            _screen = UiFactory.createUiScreen(
                name: "Dev Screen Menu",
                order: ScreenOrder.console,
                parent: transform);

            _element = UiFactory.createUiElement<DeveloperInfoMainElement>(
                name: "Menu",
                screen: _screen);
        }

        #endregion
    }
}
