using rose.row.data;
using rose.row.data.localisation;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.console;
using rose.row.ui.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rose.row.ui.ingame.screens.pause_menu
{
    public class PauseMenu : FlexibleMenu
    {
        public const float k_buttonHeight = 32f;
        public const float k_buttonWidth = 242f;
        public const float k_verticalGap = 2f;

        public const string k_WindowName = "Menus/MainOptions";
        public const string k_Resume = "Menus/Resume";
        public const string k_Quit = "Menus/Quit";

        public static void create()
        {
            var pauseMenuGameObject = new GameObject("Pause Menu");
            pauseMenuGameObject.gameObject.AddComponent<PauseMenu>();
        }

        private static readonly Dictionary<string, Action<PauseMenu>> buttons = new Dictionary<string, Action<PauseMenu>>()
        {
            { Local.get(k_Resume), (menu) => menu.setEnabled(false) },
            { "Kill", (menu) =>  FpsActorController.instance.actor.Kill(DamageInfo.Default) },
            { "Restart", (menu) => GameManager.RestartLevel() },
            { "Options", (menu) => Options.Show() }, // TODO: Implement own option menu.
            //{ "Surrender", (menu) => IngameMenuUi.instance.Surrender() },
            //{ "Back To Editor", (menu) => IngameMenuUi.instance.BackToEditor() },
            { "Menu", (menu) => GameManager.ReturnToMenu() },
            { Local.get(k_Quit), (menu) => IngameMenuUi.instance.Quit() },
        };

        public override bool pauseGame => true;
        public override bool unlockCursor => true;

        protected FlexibleMenuWindowElement _window;

        private void Start()
        {
            setEnabled(false);
        }

        public override void buildUi()
        {
            _window = UiFactory.createUiElement<FlexibleMenuWindowElement>("Window", uiScreen.gameObject.transform);
            _enabledGameObjects.Add(_window.gameObject);

            var count = buttons.Count;
            var totalHeight = k_buttonHeight * count + k_verticalGap * (count - 1);
            _window.height = totalHeight + 4 * 2;
            _window.name = Local.get(k_WindowName);
            _window.rebuild();

            var verticalOffset = totalHeight / 2;
            var currentHeight = -verticalOffset;
            foreach (var pair in buttons.Reverse())
            {
                var button = UiFactory.createUiElement<ButtonElement>(pair.Key, _window.wrapper.transform);

                button.setSize(k_buttonWidth, k_buttonHeight);
                button.setAnchoredPosition(0, currentHeight + k_buttonHeight / 2);
                button.text.setFontSize(24);
                button.text.setFont(Fonts.defaultFont);
                button.text.setColor(Color.white);

                button.onClick += (pointerEventData) => pair.Value?.Invoke(this);
                button.onClick += (pointerEventData) => setEnabled(false);

                currentHeight += k_buttonHeight + k_verticalGap;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (ConsoleManager.instance.isEnabled())
                {
                    ConsoleManager.instance.setEnabled(false);
                    return;
                }

                setEnabled(!isEnabled);
            }
        }
    }
}