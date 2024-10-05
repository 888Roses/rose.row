using rose.row.default_package;
using rose.row.easy_events;
using rose.row.easy_package.ui.factory;
using rose.row.main_menu.ui.desktop;
using rose.row.main_menu.ui.login;
using rose.row.main_menu.war.war_data;
using rose.row.ui;
using UnityEngine;
using static rose.row.easy_package.ui.factory.elements.UiElement;

namespace rose.row.main_menu.ui
{
    /// <summary>
    /// Manages the backend UI where every screen and canvas are used.
    /// </summary>
    public class MainMenuUiManager : Singleton<MainMenuUiManager>
    {
        #region static

        /// <summary>
        /// Creates a new <see cref="MainMenuUiManager"/>.
        /// </summary>
        /// <returns>
        /// The created MainMenuUiManager.
        /// </returns>
        public static MainMenuUiManager create()
        {
            var gameObject = new GameObject("Main Menu Ui Manager");
            return gameObject.AddComponent<MainMenuUiManager>();
        }

        #endregion static

        #region non-static

        public static bool isLoadingDone;
        public static int loadedMods;
        public static float contentLoadProgress
        {
            get
            {
                var activeMods = ModManager.instance.mods.Count;

                return loadedMods / (float) activeMods;
            }
        }

        private void onLoadModWorkerEndJob(LoadModWorker.State state)
        {
            loadedMods++;
        }

        public const int k_LoginIndex = 0;
        public const int k_WarMapIndex = 1;
        public const int k_CharactersIndex = 1;

        public bool isInLoginScreen() => _currentIndex == k_LoginIndex;
        public bool isInDesktop() => !isInLoginScreen();

        private int _currentIndex;
        public int currentIndex => _currentIndex;

        public void goToDesktop()
        {
            _loginScreen.setEnabled(false);
            _desktopScreen.setEnabled(true);
            _currentIndex = 1;
        }

        #region components

        /// <summary>
        /// Contains the canvas and every objects where the different ui
        /// components will be stored in.
        /// </summary>
        private UiScreen _uiScreen;

        private LoginScreen _loginScreen;
        private DesktopScreen _desktopScreen;

        public LoginScreen loginScreen => _loginScreen;
        public DesktopScreen desktopScreen => _desktopScreen;

        #endregion components

        public void initialize()
        {
            Events.onGameManagerStartLevel.after += onStartGame;
            Events.onReturnToMenu.before += onReturnToMenu;
            Events.onAllContentLoaded.after += onAllContentLoaded;
            Events.onLoadModWorkerEndJob.after += onLoadModWorkerEndJob;

            WarCityDatabase.readCities();

            // This is very important since we don't want to delete this object
            // when switching scenes. Simply, we will disable some functionalities
            // whenever a match is currently playing.
            DontDestroyOnLoad(gameObject);

            // Creates the screen canvas where all of the main ui components will
            // be stored.
            _uiScreen = UiFactory.createUiScreen(
                name: "Screen",
                order: ScreenOrder.mainMenuUi,
                parent: transform
            );

            createEssentialComponents();
        }

        private void onAllContentLoaded()
        {
            isLoadingDone = true;
        }

        private void onReturnToMenu()
        {
            _uiScreen.gameObject.SetActive(true);
        }

        private void onStartGame()
        {
            _uiScreen.gameObject.SetActive(false);
        }

        public void createEssentialComponents()
        {
            // First is going to be the login screen. This is the first window users
            // will be required to interact with since this is where they'll set
            // everything up with their account.
            _loginScreen = UiFactory.createUiElement<LoginScreen>(
                name: "Login Screen",
                screen: _uiScreen
            );

            _loginScreen.setAnchors(Anchors.FillParent);

            _desktopScreen = UiFactory.createUiElement<DesktopScreen>(
                name: "Desktop Screen",
                screen: _uiScreen
            );

            _desktopScreen.setAnchors(Anchors.FillParent);
            _desktopScreen.setEnabled(false);
        }

        #endregion non-static
    }
}