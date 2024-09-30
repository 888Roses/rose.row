using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;

namespace rose.row.main_menu.ui.desktop
{
    public class DesktopScreen : UiElement
    {
        #region components

        private DesktopWindowElement _desktopWindowElement;

        #endregion components

        public override void build()
        {
            createDesktopWindow();
        }

        #region create components

        private void createDesktopWindow()
        {
            _desktopWindowElement = UiFactory.createUiElement<DesktopWindowElement>(
                name: "Desktop Window",
                element: this
            );

            _desktopWindowElement.build();
        }

        #endregion create components

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
            _desktopWindowElement.gameObject.SetActive(_isEnabled);
        }

        #endregion
    }
}