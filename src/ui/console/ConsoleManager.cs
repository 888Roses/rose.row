using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.console.elements;
using rose.row.ui.console.elements.inputfield;
using rose.row.ui.cursor;
using rose.row.util;
using UnityEngine;

namespace rose.row.ui.console
{
    public class ConsoleManager : Singleton<ConsoleManager>
    {
        public const float k_Padding = 12f;
        public const float k_ItemHeight = 20f;
        public const int k_MaxItems = 22;

        public UiScreen uiScreen;

        public UiElement wrapper;
        public UiElement padding;
        public FlexibleListElement consoleMessagesList;
        public ConsoleInputFieldElement inputField;

        public static void create()
        {
            var console = new GameObject("Console");
            console.AddComponent<ConsoleManager>();
        }

        private void Awake()
        {
            uiScreen = UiFactory.createUiScreen(
                "Screen",
                order: ScreenOrder.console,
                parent: transform
            ).withCanvasGroup();

            wrapper = UiFactory.createGenericUiElement("Wrapper", uiScreen);
            wrapper.setAnchors(new UiElement.LiteralAnchors(0, 0.5f, 1f, 1f));
            wrapper.setPivot(0, 1);
            wrapper.image().color = ConsoleColors.background;

            padding = UiFactory.createGenericUiElement("Padding", wrapper);
            padding.setAnchors(UiElement.Anchors.FillParent);
            padding.setOffset(k_Padding, k_Padding, -k_Padding, -k_Padding);

            consoleMessagesList = UiFactory.createUiElement<FlexibleListElement>("Messages List", padding);
            consoleMessagesList.setAnchors(UiElement.Anchors.FillParent);
            consoleMessagesList.itemGap = 4f;
            consoleMessagesList.expandItemsHorizontal = true;
            consoleMessagesList.maxItems = Mathf.CeilToInt((k_MaxItems / 1920f) * Screen.height);

            inputField = UiFactory.createUiElement<ConsoleInputFieldElement>("Input Field", wrapper);
            inputField.setAnchors(UiElement.Anchors.StretchBottom);
            inputField.setPivot(0.5f, 0);
            inputField.setOffset(0, 0, 0, 0);
            inputField.setAnchoredPosition(0, -32f);
            inputField.setHeight(32f);
            inputField.rebuild();

            MouseCursor.cursorHandlers.Add(hookCursor);
        }

        private bool hookCursor(FpsActorController controller) => isEnabled();

        private void OnDestroy()
        {
            MouseCursor.cursorHandlers.Remove(hookCursor);
        }

        public void addMessage(string message)
        {
            var child = UiFactory.createUiElement<ConsoleTextElement>("Child", consoleMessagesList);
            child.setHeight(k_ItemHeight);
            child.message = message;
            consoleMessagesList.addChild(child);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            addMessage("Initialised console.");

            Console.initialize();

            setEnabled(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus) && !inputField.isFocused)
            {
                setEnabled(!isEnabled());
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                setEnabled(false);
            }
        }

        public void setEnabled(bool enabled)
        {
            uiScreen.canvasGroup.setEnabled(enabled);

            if (enabled)
            {
                inputField.inputField.inputField.Select();
                inputField.inputField.inputField.ActivateInputField();
                inputField.currentLastSentCommandsIndex = 0;

                if (FpsActorController.instance != null)
                {
                    FpsActorController.instance.DisableMovement();
                    FpsActorController.instance.DisableInput();
                }
            }
            else
            {
                inputField.inputField.inputField.DeactivateInputField();
                inputField.currentLastSentCommandsIndex = 0;

                if (FpsActorController.instance != null)
                {
                    FpsActorController.instance.EnableMovement();
                    FpsActorController.instance.EnableInput();
                }
            }
        }

        public bool isEnabled() => uiScreen.canvasGroup.isEnabled();
    }
}