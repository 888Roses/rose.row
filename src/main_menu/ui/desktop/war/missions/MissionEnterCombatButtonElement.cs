using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.main_menu.ui.desktop.war.missions
{
    public class MissionEnterCombatButtonElement : AbstractButtonElement,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// Contains all of the properties to style this button.
        /// </summary>
        public struct Properties
        {
            /// <summary>
            /// Contains properties for a certain button state.
            /// </summary>
            public struct StateProperties
            {
                public Color32 backgroundColor;
                public Color32 shadowColor;
                public Color32 textColor;
            }

            public StateProperties normal;
            public StateProperties hover;
            public StateProperties clicked;
        }

        #region states

        public abstract class DefaultButtonState : AbstractButtonState
        {
            public MissionEnterCombatButtonElement button;

            public DefaultButtonState(MissionEnterCombatButtonElement parent) : base(parent)
            {
                button = parent;
            }

            public abstract Properties.StateProperties properties { get; }

            public override void apply()
            {
                // Basic Styling.
                applyBasicStyling();

                // Styling according to the properties of that text.
                applyPropertyStyling();

                button.shadow.setRotation(180f);
            }

            protected virtual void applyPropertyStyling()
            {
                button.text.setColor(properties.textColor);
                button.shadow.image().color = properties.shadowColor;
                button.background.image().texture = ImageRegistry.warMissionButton.get();
                button.background.image().color = properties.backgroundColor;
            }

            protected virtual void applyBasicStyling()
            {
                // Text.
                button.text.setTextAlign(HorizontalAlignmentOptions.Geometry);
                button.text.setTextAlign(VerticalAlignmentOptions.Geometry);
                button.text.setFont(Fonts.defaultFont);
                button.text.setFontSize(22f);
                button.text.setFontWeight(FontWeight.Bold);
                // Force bold text and offset the letters so it doesn't look spread appart.
                button.text.text.fontStyle = FontStyles.Bold;
                button.text.setLetterSpacing(-8f);
                // For some reason the text on the reference is offset down a little bit
                // so we reproduce that by literally offseting the text here.
                button.text.setAnchoredPosition(0, -1f);
            }
        }

        public class NormalButtonState : DefaultButtonState
        {
            public NormalButtonState(MissionEnterCombatButtonElement parent) : base(parent)
            {
            }

            public override Properties.StateProperties properties => button.properties.normal;
        }

        public class HoverButtonState : DefaultButtonState
        {
            public HoverButtonState(MissionEnterCombatButtonElement parent) : base(parent)
            {
            }

            public override Properties.StateProperties properties => button.properties.hover;
        }

        public class ClickedButtonState : DefaultButtonState
        {
            public ClickedButtonState(MissionEnterCombatButtonElement parent) : base(parent)
            {
            }

            public override Properties.StateProperties properties => button.properties.clicked;

            public override void apply()
            {
                base.apply();
                button.shadow.setRotation(0f);
            }
        }

        #endregion states

        /// <summary>
        /// Properties for how to display this element.
        /// Default properties are set.
        /// </summary>
        public Properties properties = new Properties
        {
            normal = new Properties.StateProperties
            {
                backgroundColor = new Color32(252, 29, 23, 255),
                shadowColor = new Color32(87, 30, 25, 255),
                textColor = Color.white,
            },
            hover = new Properties.StateProperties
            {
                backgroundColor = new Color32(255, 10, 10, 255),
                shadowColor = new Color32(87, 30, 25, 200),
                textColor = Color.white,
            },
            clicked = new Properties.StateProperties
            {
                backgroundColor = new Color32(255, 10, 10, 255),
                shadowColor = new Color32(87, 30, 25, 225),
                textColor = Color.white,
            }
        };

        public string buttonText;
        public Action onClicked;

        private NormalButtonState _normalState;
        private HoverButtonState _hoverState;
        private ClickedButtonState _clickedState;

        #region components

        private UiElement _background;
        private UiElement _shadow;
        private TextElement _text;
        private bool _isHovered;

        public UiElement background => _background;
        public UiElement shadow => _shadow;
        public TextElement text => _text;

        #endregion components

        protected override void Awake()
        {
            _normalState = new NormalButtonState(this);
            _hoverState = new HoverButtonState(this);
            _clickedState = new ClickedButtonState(this);

            createElements();
        }

        private void createElements()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);

            _shadow = UiFactory.createGenericUiElement("Shadow", this);
            _shadow.setAnchors(Anchors.FillParent);
            _shadow.image().texture = ImageRegistry.fieldShadow.get();

            _text = UiFactory.createUiElement<TextElement>("Text", this);
            _text.setAnchors(Anchors.FillParent);
            _text.build();
        }

        public override void build()
        {
            _text.setText(buttonText);
        }

        private void Start()
        {
            setState(_normalState);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            setState(_hoverState);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            setState(_normalState);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            setState(_clickedState);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isHovered)
            {
                setState(_hoverState);
                onClicked?.Invoke();
            }
            else
            {
                setState(_normalState);
            }
        }
    }
}