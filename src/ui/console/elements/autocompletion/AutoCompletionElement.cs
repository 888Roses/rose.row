using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using System;
using UnityEngine;

namespace rose.row.ui.console.elements.autocompletion
{
    public class AutoCompletionElement : UiElement
    {
        public const int k_BorderWidth = 1;
        public const int k_Padding = 12;

        public CanvasGroup group;
        public UiElement border;
        public UiElement wrapper;
        public UiElement padding;
        public FlexibleListElement suggestionList;

        public int selectedSuggestionIndex = 0;

        public bool isEnabled => group.isEnabled();

        public void setEnabled(bool enabled) => group.setEnabled(enabled);

        public Action<AutoCompletionSuggestionElement> onValidateSuggestion;

        public override void build()
        {
            group = use<CanvasGroup>();

            border = UiFactory.createGenericUiElement("Border", this);
            border.setAnchors(Anchors.FillParent);
            border.setBackgroundColor("#282828AA");

            wrapper = UiFactory.createGenericUiElement("Wrapper", border);
            wrapper.setAnchors(Anchors.FillParent);
            wrapper.setOffset(k_BorderWidth, k_BorderWidth, -k_BorderWidth, -k_BorderWidth);
            border.setBackgroundColor("#0A0A0AEE");

            padding = UiFactory.createGenericUiElement("Padding", wrapper);
            padding.setAnchors(Anchors.FillParent);
            padding.setOffset(k_Padding, k_Padding, -k_Padding, -k_Padding);

            suggestionList = UiFactory.createUiElement<FlexibleListElement>("List", padding);
            suggestionList.setAnchors(Anchors.FillParent);
            suggestionList.itemGap = 0f;
            suggestionList.expandItemsHorizontal = true;
        }

        public float getHeight()
        {
            return suggestionList.count * 24f + 2 * k_BorderWidth + 2 * k_Padding;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                int index = selectedSuggestionIndex - 1;
                if (index < 0)
                    index = suggestionList.children.Count - 1;

                setSelectedItem(index);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                int index = selectedSuggestionIndex + 1;
                if (index >= suggestionList.children.Count)
                    index = 0;

                setSelectedItem(index);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                validateSuggestion();
            }
        }

        public AutoCompletionSuggestionElement selected
        {
            get
            {
                if (suggestionList.children.Count == 0)
                    return null;

                selectedSuggestionIndex = Mathf.Clamp(selectedSuggestionIndex, 0, suggestionList.children.Count - 1);
                var last = suggestionList[selectedSuggestionIndex];

                if (last == null)
                    return null;

                return last as AutoCompletionSuggestionElement;
            }
        }

        private void validateSuggestion()
        {
            onValidateSuggestion?.Invoke(selected);
        }

        public void setSelectedItem(int index)
        {
            selectedSuggestionIndex = index;
            updateSelectedItem();
        }

        public void updateSelectedItem()
        {
            for (int i = 0; i < suggestionList.children.Count; i++)
            {
                if (suggestionList.children[i] is AutoCompletionSuggestionElement element)
                {
                    var selected = i == selectedSuggestionIndex;
                    element.setSelected(selected);
                }
            }
        }

        public AutoCompletionSuggestionElement createSuggestionItem(string suggestion) => createSuggestionItem(suggestion, suggestion);

        public AutoCompletionSuggestionElement createSuggestionItem(string suggestion, string completedText)
        {
            var suggestionElement = UiFactory.createUiElement<AutoCompletionSuggestionElement>("Child", suggestionList);
            suggestionElement.setSuggestion(suggestion, completedText);

            suggestionElement.onHovered += () =>
            {
                setSelectedItem(suggestionList.children.IndexOf(suggestionElement));
            };

            suggestionElement.onClicked += () =>
            {
                validateSuggestion();
            };

            suggestionList.addChild(suggestionElement);
            updateSelectedItem();
            return suggestionElement;
        }

        public void clearSuggestionItems()
        {
            // TODO: Implement that directly into the FlexibleListElement instead.
            foreach (var item in suggestionList.children)
                Destroy(item.gameObject);

            suggestionList.children.Clear();
        }
    }
}