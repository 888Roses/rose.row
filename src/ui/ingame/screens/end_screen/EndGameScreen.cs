using rose.row.actor.player.camera;
using rose.row.data;
using rose.row.data.localisation;
using rose.row.default_package;
using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.match;
using rose.row.ui.cursor;
using rose.row.ui.ingame.screens.death_screen;
using System;
using TMPro;
using UnityEngine;

namespace rose.row.ui.ingame.screens.end_screen
{
    public class EndGameScreen : Singleton<EndGameScreen>
    {
        public const int k_StarCount = 5;
        public const int k_StarSize = 61;
        public const float k_GlobalOffsetY = 44f;
        public const string k_RateTextContent = "Menus/GameOverScreen/Title";

        private UiScreen _screen;
        private UiElement _starContainer;
        private StarElement[] _stars;
        private CloseWindowButton _quitButton;
        private UiElement _wrapper;
        private TextElement _rateText;

        public static void create()
        {
            var gameObject = new GameObject("End Game Screen");
            gameObject.AddComponent<EndGameScreen>();
        }

        public static void subscribeToInitializationEvents()
        {
            Events.onGameManagerStartLevel.after += () =>
            {
                create();
            };

            MouseCursor.cursorHandlers.Add((e) => instance != null && instance.isEnabled);
        }

        private void Awake()
        {
            build();

            Events.onMatchEnded.after += onGameEnd;
        }

        private void OnDestroy()
        {
            Events.onMatchEnded.after -= onGameEnd;
        }

        private void onGameEnd(int winner, bool allowContinueBattle)
        {
            if (winner == CurrentMatch.playerTeam)
            {
                Audio.play(AudioRegistry.win.get());
            }

            VictoryUi.instance.victoryContainer.SetActive(false);

            foreach (var actor in ActorManager.instance.actors)
            {
                if (actor.IsSeated())
                    actor.LeaveSeat(false);

                if (actor.aiControlled)
                    actor.Deactivate();
            }

            GameManager.FreezeGameplay();

            setEnabled(true);
            Destroy(DeathScreen.instance.gameObject);
            DeathCamera.instance.enable();
            FpsActorController.instance.DisableCameras();
            FpsActorController.instance.DisableInput();
            FpsActorController.instance.DisableMovement();
            FpsActorController.instance.actor.Freeze();
            FpsActorController.instance.actor.Hide();
        }

        private void Start()
        {
            setEnabled(false);
        }

        #region building

        public float maxSizeX => k_StarCount * k_StarSize;

        private void build()
        {
            createScreen();
            createWrapper();

            createStarsContainer();
            createStars();

            createRateText();
            createQuitButton();
        }

        private void createScreen()
        {
            _screen = UiFactory.createUiScreen(
                name: "Screen",
                order: ScreenOrder.endGameScreen,
                parent: transform
            );
        }

        private void createWrapper()
        {
            _wrapper = UiFactory.createGenericUiElement("Wrapper", _screen);
            _wrapper.setAnchors(UiElement.Anchors.FillParent);
            _wrapper.setAnchoredPosition(0, k_GlobalOffsetY);
        }

        private void createRateText()
        {
            _rateText = UiFactory.createUiElement<TextElement>("Text", _wrapper);
            _rateText.setSize(maxSizeX, 24);
            _rateText.setAnchoredPosition(0, 51.5f);
            _rateText.build();

            _rateText.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _rateText.setTextAlign(VerticalAlignmentOptions.Geometry);
            _rateText.setFont(Fonts.defaultFont);
            _rateText.setColor(Color.white);
            _rateText.setText(Local.get(k_RateTextContent));
            _rateText.setFontSize(30f);
            _rateText.setFontWeight(FontWeight.Bold);
            _rateText.setShadow(new Vector2(1, -1), Color.black, true);
        }

        private void createStarsContainer()
        {
            _starContainer = UiFactory.createGenericUiElement("Star Container", _wrapper);
            _starContainer.setSize(maxSizeX, k_StarSize);
        }

        private void createStars()
        {
            _stars = new StarElement[k_StarCount];
            for (int i = 0; i < k_StarCount; i++)
            {
                var storedI = i;
                _stars[i] = createStar(
                    name: $"Star {i}",
                    element: _starContainer,
                    onHovered: () =>
                    {
                        for (int j = 0; j < k_StarCount; j++)
                            _stars[j].icon.gameObject.SetActive(j >= storedI);
                    }
                );

                var xOffset = i * k_StarSize;
                var posX = maxSizeX / 2 - xOffset;
                _stars[i].setAnchoredPosition(posX - k_StarSize / 2, 0);
            }
        }

        private void createQuitButton()
        {
            _quitButton = UiFactory.createUiElement<CloseWindowButton>("Close Window Button", _wrapper);
            _quitButton.setAnchors(UiElement.Anchors.MiddleCenter, false, false);
            _quitButton.setPivot(0.5f, 0.5f);
            _quitButton.setAnchoredPosition(0, -54);
        }

        public StarElement createStar(string name, UiElement element, Action onHovered)
        {
            var star = UiFactory.createUiElement<StarElement>(name, element);
            star.setSize(k_StarSize);
            star.onHovered += onHovered;

            return star;
        }

        #endregion building

        #region enabled state

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
            var group = _wrapper.use<CanvasGroup>();
            group.interactable = _isEnabled;
            group.blocksRaycasts = _isEnabled;
            group.alpha = _isEnabled ? 1 : 0;
        }

        #endregion enabled state
    }
}