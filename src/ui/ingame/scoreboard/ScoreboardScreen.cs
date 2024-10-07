using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.util;
using UnityEngine;
using static rose.row.easy_package.ui.factory.elements.UiElement;

namespace rose.row.ui.ingame.scoreboard
{
    /// <summary>
    /// Reponsible for creating <see cref="ScoreboardElement"/> and managing the input
    /// part of the scoreboard.
    /// </summary>
    public class ScoreboardScreen : Singleton<ScoreboardScreen>
    {
        public static void create()
        {
            var gameObject = new GameObject("Scoreboard");
            gameObject.AddComponent<ScoreboardScreen>();
        }

        private void Awake()
        {
            build();

            setEnabled(false);
        }

        private void Update()
        {
            setEnabled(SteelInput.GetButton(SteelInput.KeyBinds.Scoreboard));
        }

        #region disable/enable

        // Responsible for managing whether the scoreboard is enabled or disabled.

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            updateEnabledState();
        }

        private void updateEnabledState()
        {
            _uiScreen.canvasGroup.setEnabled(_isEnabled);
        }

        #endregion disable/enable

        #region element

        // Responsible for everything that has to do with creating and managing the
        // UI element.

        private UiScreen _uiScreen;
        private ScoreboardElement _element;
        public ScoreboardElement element => _element;

        private void build()
        {
            createUiScreen();
            createUiElement();
        }

        private void createUiScreen()
        {
            _uiScreen = UiFactory.createUiScreen(
                name: "Screen",
                order: ScreenOrder.leaderboard,
                parent: transform
            ).withCanvasGroup();
        }

        private void createUiElement()
        {
            _element = UiFactory.createUiElement<ScoreboardElement>(
                name: "Scoreboard",
                screen: _uiScreen
            );
            _element.setAnchors(Anchors.FillParent);
        }

        #endregion element
    }
}