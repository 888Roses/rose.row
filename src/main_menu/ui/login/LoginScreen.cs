using rose.row.client;
using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.main_menu.ui.login.elements;
using UnityEngine;

namespace rose.row.main_menu.ui.login
{
    public class LoginScreen : UiElement
    {
        public static readonly Color32 textColor = new Color32(218, 212, 207, 255);

        #region components

        /// <summary>
        /// Responsible for showing a random background image on startup.
        /// </summary>
        private RandomImageBackground _imageBackground;

        /// <summary>
        /// Responsible for allowing the user to login an account.
        /// </summary>
        private LoginWindowElement _loginWindowElement;

        #endregion components

        /// <summary>
        /// Builds the entire window's UI.
        /// </summary>
        public override void build()
        {
            initializeImageBackground();
            createLoginWindow();
        }

        #region create components

        /// <summary>
        /// Creates and updates the background element responsible for showing a
        /// random image on startup.
        /// </summary>
        private void initializeImageBackground()
        {
            _imageBackground = UiFactory.createUiElement<RandomImageBackground>(
                name: "Background",
                parent: transform
            );

            _imageBackground.setAnchors(Anchors.FillParent);

            _imageBackground.initialize(new Texture2D[]
            {
                ImageRegistry.mainMenuBackground0.get(),
                ImageRegistry.mainMenuBackground1.get(),
                ImageRegistry.mainMenuBackground2.get(),
            });
        }

        /// <summary>
        /// Creates and updates the login screen window.
        /// </summary>
        private void createLoginWindow()
        {
            _loginWindowElement = UiFactory.createUiElement<LoginWindowElement>(
                name: "Login Window",
                parent: transform
            );

            _loginWindowElement.build();
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
            _imageBackground.gameObject.SetActive(_isEnabled);
            _loginWindowElement.gameObject.SetActive(_isEnabled);
        }

        #endregion

        public void login()
        {
            Client.name = _loginWindowElement.playerNameInputField.inputField.inputField.text;
            if (string.IsNullOrEmpty(Client.name)
            || string.IsNullOrWhiteSpace(Client.name))
            {
                return;
            }

            PlayerPrefs.SetString("client_input_custom_name", Client.name);
            MainMenuUiManager.instance.goToDesktop();
        }
    }
}