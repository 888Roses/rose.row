using rose.row.data;
using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.console.elements.inputfield;
using rose.row.ui.cursor;
using rose.row.util;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        public ScrollableElement consoleMessagesList;
        public VerticalListElement consoleMessagesListContent;
        public ConsoleInputFieldElement inputField;

        public static void create()
        {
            var console = new GameObject("Console");
            console.AddComponent<ConsoleManager>();
            DontDestroyOnLoad(console.gameObject);
        }

        private void Awake()
        {
            uiScreen = UiFactory.createUiScreen(
                "Screen",
                order: ScreenOrder.console,
                parent: transform
            ).withCanvasGroup();

            wrapper = UiFactory.createGenericUiElement("Wrapper", uiScreen);
            wrapper.setAnchors(UiElement.Anchors.FillParent);
            wrapper.setPivot(0, 1);
            wrapper.setBackgroundColor("#0A0A0ACC");

            padding = UiFactory.createGenericUiElement("Padding", wrapper);
            padding.setAnchors(UiElement.Anchors.FillParent);
            padding.setOffset(k_Padding, k_Padding, -k_Padding, -k_Padding);
            padding.image();
            var mask = padding.use<Mask>();
            mask.showMaskGraphic = false;

            consoleMessagesList = UiFactory.createUiElement<ScrollableElement>("Messages List", padding);
            consoleMessagesList.build();
            consoleMessagesList.setAnchors(UiElement.Anchors.FillParent);
            consoleMessagesList.setOffset(0, 32f, 0, 0);

            consoleMessagesListContent = UiFactory.createUiElement<VerticalListElement>("Content", consoleMessagesList);
            consoleMessagesListContent.build();
            consoleMessagesListContent.setChildControlWidth(true);
            consoleMessagesListContent.setChildForceExpandWidth(true);
            consoleMessagesList.setContent(consoleMessagesListContent);
            consoleMessagesListContent.setAnchors(UiElement.Anchors.StretchTop);

            inputField = UiFactory.createUiElement<ConsoleInputFieldElement>("Input Field", wrapper);
            inputField.setAnchors(UiElement.Anchors.StretchBottom);
            inputField.setPivot(0.5f, 0);
            inputField.setOffset(0, 0, 0, 0);
            inputField.setAnchoredPosition(0, 0);
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
            var text = UiFactory.createUiElement<TextElement>("Message", consoleMessagesListContent);
            text.build();
            text.setColor(Console.getColorForLogType(LogType.Log));
            text.setFontSize(16, true);
            text.setFont(Fonts.consoleFont);
            text.setAllowRichText(true);

            text.setAdaptiveHeight();
            text.setText(message);

            LayoutRebuilder.MarkLayoutForRebuild(text.rectTransform);
            StartCoroutine(updateLayout());
        }

        private IEnumerator updateLayout()
        {
            yield return new WaitForEndOfFrame();

            LayoutRebuilder.MarkLayoutForRebuild(consoleMessagesListContent.rectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(consoleMessagesList.rectTransform);

            consoleMessagesList.scrollRect.verticalNormalizedPosition = 0;
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

            // We only want to handle closing the console using the Escape key in the console if the player is not in a game.
            // If they are in a match, then we can relly on the Pause Menu to do so.
            // TODO: This is not very good code, and I would prefer to have a way to "suppress" sort of events like opening the pause menu.
            // This way, the console could close and "suppress" opening the pause menu.
            if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.IsIngame())
            {
                setEnabled(false);
            }
        }

        public void setEnabled(bool enabled)
        {
            Debug.Log("Setting console enabled " + enabled);

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