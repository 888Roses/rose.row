using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.ui;
using rose.row.ui.cursor;
using UnityEngine;
using static rose.row.easy_package.ui.factory.elements.UiElement;

namespace rose.row.dev.dev_weapon_selec
{
    public class WeaponSelectionScreen : Singleton<WeaponSelectionScreen>
    {
        public static void create()
        {
            var gameObject = new GameObject("Weapon Selection Screen");
            gameObject.AddComponent<WeaponSelectionScreen>();
        }

        private UiScreen _uiScreen;
        private WeaponSelectionElement _selectionElement;

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        private void Awake()
        {
            _uiScreen = UiFactory.createUiScreen(
                name: "Screen",
                order: ScreenOrder.weaponSelectionMenu,
                parent: transform
            );

            MouseCursor.cursorHandlers.Add(hookCursor);

            setEnabled(false);
        }

        private void OnDestroy()
        {
            MouseCursor.cursorHandlers.Remove(hookCursor);
        }

        private bool hookCursor(FpsActorController c) => _isEnabled;


        private void Start()
        {
            _selectionElement = UiFactory.createUiElement<WeaponSelectionElement>(
                name: "Weapon Selection",
                screen: _uiScreen
            );
            _selectionElement.build();
            _selectionElement.setAnchors(Anchors.FillParent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                setEnabled(!isEnabled);
            }
        }

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            _uiScreen.gameObject.SetActive(enabled);
        }
    }
}