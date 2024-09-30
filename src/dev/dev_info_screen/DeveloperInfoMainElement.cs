using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System.Collections.Generic;

namespace rose.row.dev.dev_info_screen
{
    public class DeveloperInfoMainElement : UiElement
    {
        public static DeveloperInfoMainElement instance
            => DeveloperInfoScreen.instance.main;

        public Dictionary<Shortcut, AbstractDeveloperMenu> menus
            = new Dictionary<Shortcut, AbstractDeveloperMenu>();

        private AbstractDeveloperMenu _currentMenu;
        private UiElement _wrapper;
        public UiElement wrapper => _wrapper;

        public override void build()
        {
            setAnchors(Anchors.FillParent);
            _wrapper = UiFactory.createGenericUiElement("Wrapper", this);
            _wrapper.setAnchors(Anchors.FillParent);
        }

        public T createMenu<T>(string name) where T : AbstractDeveloperMenu
        {
            return UiFactory.createUiElement<T>(name, _wrapper);
        }

        public void register(AbstractDeveloperMenu menu, Shortcut activateKey)
        {
            menus.Add(activateKey, menu);
            setMenusDisabled();
        }

        private void setMenuEnabled(AbstractDeveloperMenu menu)
        {
            _currentMenu = menu;

            foreach (var otherMenu in menus.Values)
            {
                otherMenu.gameObject.SetActive(otherMenu == menu);
            }
        }

        private void setMenusDisabled()
        {
            foreach (var otherMenu in menus.Values)
                if (otherMenu != null)
                    otherMenu.gameObject.SetActive(false);
            _currentMenu = null;
        }

        private void Update()
        {
            foreach (var keySet in menus)
            {
                var keycode = keySet.Key;
                var menu = keySet.Value;

                if (keycode.down())
                {
                    if (_currentMenu == menu)
                    {
                        setMenusDisabled();
                        return;
                    }

                    setMenuEnabled(menu);
                    return;
                }
            }
        }
    }
}