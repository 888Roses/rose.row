using rose.row.data;
using rose.row.data.mod;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.main_menu.ui.abstraction.elements;
using TMPro;
using UnityEngine;

namespace rose.row.main_menu.ui.login.elements
{
    public class LoginWindowElement : AbstractWindowElement
    {
        #region calculate correct size

        // Calculates the size of the login window based on the height
        // of the main window since this is how the background (where
        // the blur is) scales with different resolutions.
        public static float windowSizeX
        {
            get
            {
                const float w = 460f;
                return Screen.height * (w / 1080f);
            }
        }

        public static float windowSizeY
        {
            get
            {
                const float h = 700f;
                return Screen.height * (h / 1080f);
            }
        }

        public override float anchorX => 0.5f;
        public override float anchorY => 0.5f;

        #endregion calculate correct size

        public const float k_WrapperOffsetY = 44f;
        public const float k_TitleHeight = 53f;
        public const float k_TitleHeightOffset = 4f;

        #region components

        private BorderedUiElement _background;
        private LoginButtonElement _loginButton;
        private TextElement _titleText;
        private ProgressBarElement _modLoadingProgress;

        private UiElement _loginSection;
        private LoginInputFieldElement _playernameInputField;
        private LoginInputFieldElement _passwordInputField;

        #endregion components

        #region creating components

        public override void build()
        {
            base.build();

            createBackground();

            createTitle();
            createLoginSection();

            createLoginButton();
            createModLoadingProgress();
        }
        protected void createLoginSection()
        {
            _loginSection = UiFactory.createGenericUiElement("Login Section", _wrapper);
            _loginSection.setAnchors(Anchors.StretchTop);
            _loginSection.setPivot(0, 1);
            _loginSection.setHeight(relativeHeight(400));
            _loginSection.setAnchoredPosition(0, relativeHeight(-k_TitleHeight - k_TitleHeightOffset));

            createPlayernameInputField();
            createPasswordInputField();
        }

        protected void createPlayernameInputField()
        {
            _playernameInputField = createInputField("Player Name", "Email/Playername");
        }

        protected void createPasswordInputField()
        {
            _passwordInputField = createInputField("Password Field", "Password");
            _passwordInputField.setAnchoredPosition(0, relativeHeight(_passwordInputField.anchoredPosition.y - 50f - 12f));
        }

        protected virtual LoginInputFieldElement createInputField(string name, string placeholder)
        {
            var inputField = UiFactory.createUiElement<LoginInputFieldElement>(name, _loginSection);
            inputField.placeholder = placeholder;
            inputField.build();

            inputField.setAnchors(Anchors.TopCenter);
            inputField.setPivot(0.5f, 1);
            inputField.setSize(relativeWidth(392f), relativeHeight(50f));

            return inputField;
        }

        protected void createTitle()
        {
            _titleText = UiFactory.createUiElement<TextElement>("Title", _wrapper);
            _titleText.setFont(Fonts.defaultFont);
            _titleText.setFontSize(32f);
            _titleText.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _titleText.setTextAlign(VerticalAlignmentOptions.Geometry);
            _titleText.setColor(LoginScreen.textColor);
            _titleText.setFontWeight(FontWeight.Bold);
            _titleText.setText("LOGIN");
            _titleText.setAnchors(Anchors.StretchTop);
            _titleText.setPivot(0, 1);
            _titleText.setHeight(relativeHeight(k_TitleHeight));
            _titleText.setAnchoredPosition(0, relativeHeight(-k_TitleHeightOffset));
            _titleText.setShadow(new Vector2(1, -1), new Color(0, 0, 0, 0.5f));
        }

        protected void createLoginButton()
        {
            _loginButton = UiFactory.createUiElement<LoginButtonElement>("Login Button", _wrapper);

            _loginButton.setPivot(0.5f, 0);
            _loginButton.setAnchors(Anchors.BottomCenter);
            _loginButton.setSize(relativeWidth(392f), relativeHeight(90.5f));
            _loginButton.setAnchoredPosition(0, relativeHeight(57.6f));

            _loginButton.updateInformation();
        }

        private void createModLoadingProgress()
        {
            _modLoadingProgress = UiFactory.createUiElement<ProgressBarElement>("Mod Loading Progress", _wrapper);
            _modLoadingProgress.setPivot(0.5f, 0);
            _modLoadingProgress.setAnchors(Anchors.BottomCenter);
            _modLoadingProgress.setSize(relativeWidth(392f), relativeHeight(90.5f / 4));
            _modLoadingProgress.setAnchoredPosition(0, relativeHeight(57.6f));
            _modLoadingProgress.fill.image().color = new Color32(244, 169, 0, 255);
        }

        private void updateModLoadingProgress()
        {
            if (ModHelper.isLoading)
            {
                _modLoadingProgress.gameObject.SetActive(true);
                _loginButton.gameObject.SetActive(false);

                _modLoadingProgress.setProgress(ModHelper.progress);
            }
            else
            {
                _modLoadingProgress.gameObject.SetActive(false);
                _loginButton.gameObject.SetActive(ModHelper.hasLoadedOnce);
            }
        }

        protected override void createWrapper()
        {
            base.createWrapper();
            _wrapper.setSize(windowSizeX, windowSizeY);
            _wrapper.setAnchoredPosition(0, relativeHeight(-k_WrapperOffsetY));
        }

        protected void createBackground()
        {
            // Creating a background with a border.
            _background = UiFactory.createUiElement<BorderedUiElement>(
                name: "Background",
                element: _wrapper
            );
            _background.setAnchors(Anchors.FillParent);
            _background.backgroundColor = new Color(.1f, .1f, .1f, 0.45f);
            _background.borderColor = new Color(1, 1, 1, 0.04f);
            _background.borderCorners = false;
            _background.thickness = 2;
            // Some elements do not get built immediately so we need to do that manually.
            _background.build();
        }

        public void updateSizes()
        {
            _wrapper.setSize(windowSizeX, windowSizeY);
            _wrapper.setAnchoredPosition(0, relativeHeight(-k_WrapperOffsetY));

            _titleText.setHeight(relativeHeight(k_TitleHeight));
            _titleText.setAnchoredPosition(0, relativeHeight(-k_TitleHeightOffset));

            _loginSection.setHeight(relativeHeight(400));
            _loginSection.setAnchoredPosition(0, relativeHeight(-k_TitleHeight - k_TitleHeightOffset));
            _playernameInputField.setSize(relativeWidth(392f), relativeHeight(50f));
            _passwordInputField.setSize(relativeWidth(392f), relativeHeight(50f));
            _passwordInputField.setAnchoredPosition(0, relativeHeight(_playernameInputField.anchoredPosition.y - 50f - 12f));

            _loginButton.setSize(relativeWidth(392f), relativeHeight(90.5f));
            _loginButton.setAnchoredPosition(0, relativeHeight(57.6f));
        }

        private void Update()
        {
            updateSizes();
            updateModLoadingProgress();
        }

        #endregion creating components
    }
}