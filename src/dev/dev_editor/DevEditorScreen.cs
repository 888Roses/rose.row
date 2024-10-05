using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.ui;
using rose.row.util;
using UnityEngine;

namespace rose.row.dev.dev_editor
{
    public class DevEditorScreen : Singleton<DevEditorScreen>
    {
        public static void create()
        {
            var gameObject = new GameObject("Dev Editor");
            gameObject.AddComponent<DevEditorScreen>();
            DontDestroyOnLoad(gameObject);
        }

        #region enable/disable

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            updateEnabledState();
        }

        private void updateEnabledState()
        {
            _screen.canvasGroup.setEnabled(_isEnabled);
        }

        #endregion

        private void Awake()
        {
            build();
        }

        private void Start()
        {
            setEnabled(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                setEnabled(!_isEnabled);
            }
        }

        #region building

        private UiScreen _screen;

        private void build()
        {
            _screen = UiFactory.createUiScreen("Dev Editor", ScreenOrder.devEditor, transform).withCanvasGroup();
            UiFactory.createUiElement<MainWindow>("Main Window", _screen);
            UiFactory.createUiElement<OverlayImageWindow>("Overlay Image Window", _screen);
        }

        #endregion
    }
}