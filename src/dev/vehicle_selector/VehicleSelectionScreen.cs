using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.ui;
using rose.row.ui.cursor;
using UnityEngine;
using static rose.row.easy_package.ui.factory.elements.UiElement;

namespace rose.row.dev.vehicle_selector
{
    public class VehicleSelectionScreen : Singleton<VehicleSelectionScreen>
    {
        public static void create()
        {
            var gameObject = new GameObject("Vehicle Selection Screen");
            gameObject.AddComponent<VehicleSelectionScreen>();
            //DontDestroyOnLoad(gameObject);
        }

        private UiScreen _uiScreen;
        private VehicleSelectionElement _selectionElement;

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        private void Awake()
        {
            _uiScreen = UiFactory.createUiScreen(
                name: "Screen",
                order: ScreenOrder.vehicleSelectionMenu,
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
            _selectionElement = UiFactory.createUiElement<VehicleSelectionElement>(
                name: "Vehicle Selection",
                screen: _uiScreen
            );
            _selectionElement.build();
            _selectionElement.setAnchors(Anchors.FillParent);
        }

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            _uiScreen.gameObject.SetActive(enabled);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                setEnabled(!isEnabled);
            }
        }
    }
}