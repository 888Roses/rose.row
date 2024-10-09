using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.easy_package.ui.text;
using rose.row.ui.console.elements.autocompletion;
using System.Text;
using UnityEngine;

namespace rose.row.ui.console.elements.inputfield
{
    public class ConsoleInputFieldElement : UiElement
    {
        public InputFieldElement inputField;
        public AutoCompletionElement completion;
        public TextElement completionText;

        public bool isFocused => inputField.isFocused;
        public int currentLastSentCommandsIndex = 0;

        private void setCurrentCommand(string command)
        {
            setContent(command);
            inputField.inputField.caretPosition = inputField.inputField.text.Length;
            updateCompletionText(command);
        }

        private void Update()
        {
            if ((Input.GetKey(KeyCode.LeftAlt) || string.IsNullOrEmpty(inputField.inputField.text)) && inputField.isFocused)
            {
                if (Console.sentCommands.Count == 0)
                    return;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currentLastSentCommandsIndex--;
                    if (currentLastSentCommandsIndex < 0)
                        currentLastSentCommandsIndex = Console.sentCommands.Count - 1;
                    setCurrentCommand(Console.sentCommands[currentLastSentCommandsIndex]);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currentLastSentCommandsIndex++;
                    if (currentLastSentCommandsIndex >= Console.sentCommands.Count)
                        currentLastSentCommandsIndex = 0;
                    setCurrentCommand(Console.sentCommands[currentLastSentCommandsIndex]);
                }
            }
        }

        public override void build()
        {
            image().color = ConsoleColors.background;

            inputField = UiFactory.createUiElement<InputFieldElement>("Input Field", this);
            inputField.setAnchors(Anchors.FillParent);
            inputField.font = Fonts.consoleFont;
            inputField.textColor = ConsoleColors.inputFieldText;
            inputField.placeholderColor = ConsoleColors.inputFieldPlaceholder;
            inputField.padding = new Vector2(ConsoleManager.k_Padding, 0);
            inputField.build();

            completionText = InputFieldElement.createInputFieldText(
                "Completion Text",
                inputField.viewport,
                Fonts.consoleFont,
                16f,
                ConsoleColors.autoCompletionSuggestionText,
                false
            );

            completion = UiFactory.createUiElement<AutoCompletionElement>("Auto Completion", this);
            completion.setAnchors(Anchors.StretchBottom);
            completion.setPivot(0.5f, 0);
            // Height relative to the window's height.
            var h = (400f / 1920f) * Screen.height;
            completion.setHeight(h);
            completion.setAnchoredPosition(0, -h);
            completion.onValidateSuggestion += onCompletionValidateSuggestion;
            completion.setEnabled(false);

            inputField.inputField.onValueChanged.AddListener(onValueChanged);
            inputField.inputField.onEndEdit.AddListener((value) => updateCompletionVisibility());
            inputField.inputField.onSelect.AddListener((value) => updateCompletionVisibility());
            inputField.inputField.onSubmit.AddListener(onSubmit);
        }

        private void onSubmit(string value)
        {
            Console.executeCommand(value);
            clearField();

            inputField.inputField.Select();
            inputField.inputField.ActivateInputField();

            currentLastSentCommandsIndex = 0;
        }

        public void setContent(string value)
        {
            inputField.inputField.text = value;
            updateCompletionVisibility();
        }

        private void clearField()
        {
            inputField.inputField.text = "";
            updateCompletionVisibility();
        }

        private void updateCompletionVisibility()
        {
            var visible =
                completion.suggestionList.count > 0 &&
                inputField.isFocused &&
                !string.IsNullOrEmpty(inputField.inputField.text) &&
                completion.selected != null;

            completionText.gameObject.SetActive(visible);
            completion.setEnabled(visible);
        }

        private void onCompletionValidateSuggestion(AutoCompletionSuggestionElement element)
        {
            if (element == null || element.completedText == null)
                return;

            inputField.inputField.DeactivateInputField();
            inputField.inputField.Select();
            inputField.inputField.ActivateInputField();

            inputField.inputField.text = element.completedText;
            inputField.inputField.caretPosition = inputField.inputField.text.Length;
        }

        private void onValueChanged(string value)
        {
            completion.setEnabled(true);
            updateCompletion(value);
        }

        public void updateCompletion(string value)
        {
            CommandSuggestionProvider.populateSuggestedCommands(value, completion);
            updateCompletionText(value);
            updateCompletionVisibility();
        }

        private void updateCompletionText(string value)
        {
            if (completion.selected == null)
                return;

            var currentTextBuilder = new StringBuilder();
            var completionTextBuilder = new StringBuilder();
            var suggestionText = completion.selected.suggestion.Contains(" ")
                ? completion.selected.completedText : completion.selected.suggestion;

            for (int i = 0; i < suggestionText.Length; i++)
            {
                if (i >= value.Length)
                {
                    completionTextBuilder.Append(suggestionText[i]);
                }
                else
                {
                    if (value[i] == ' ' && i == value.Length - 1)
                        break;

                    currentTextBuilder.Append(value[i]);
                }
            }

            var transparentStyle = TextStyle.empty.withColor(Color.clear);
            var completionStyle = TextStyle.empty.withColor(ConsoleColors.autoCompletionSuggestionText);
            var component = new TextComponent(currentTextBuilder.ToString()).withStyle(transparentStyle);
            component.append(new TextComponent(completionTextBuilder.ToString()).withStyle(completionStyle));
            completionText.setText(component.getString());
        }

        public void rebuild()
        {
            inputField.updateSize();
        }
    }
}