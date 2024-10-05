using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System.IO;
using UnityEngine;

namespace rose.row.dev.dev_editor
{
    public class OverlayImageWindow : FloatingWindow
    {
        public override string name => "Overlay Background Image";
        public override bool isGameOnly => false;
        public override float headerHeight => 32f;
        public override float containerPadding => 16f;
        public override UiElement itemContainer => _list;
        public const float k_ItemGap = 8f;

        private Texture2D _texture;

        #region building

        private VerticalListElement _list;
        private UiElement _backgroundImage;
        private Button _loadImageButton;

        public override void build()
        {
            base.build();

            _list = UiFactory.createUiElement<VerticalListElement>("List", container);
            _list.build();
            _list.setAnchors(Anchors.FillParent);
            _list.setChildControlWidth(true);
            _list.setChildForceExpandWidth(true);
            _list.setSpacing(k_ItemGap);

            _loadImageButton = button("Load Image", true, loadImage);
            button("Remove Image", false, removeDisplayedImage);

            slider("Opacity").onValueChanged += onOpacityValueChanged;

            setSize(400, 200);
            setAnchoredPosition(Screen.width / 2 - getWidth() / 2 - 24, Screen.height / 2 - getHeight() / 2 - 24);

            updateHeight(_list.rectTransform, k_ItemGap);
        }

        private void onOpacityValueChanged(float value)
        {
            createBackgroundImageIfNull();

            _backgroundImage.use<CanvasGroup>().alpha = value;
        }

        private static string imageDirectoryPath => $"{Constants.basePath}/Textures/dev";

        private void loadImage()
        {
            var files = Directory.GetFiles(imageDirectoryPath, "*.png");

            if (files.Length == 0)
            {
                Debug.LogWarning($"Could not find any image at path: '{imageDirectoryPath}'!");
                _loadImageButton.setText($"Couldn't find :(");
                return;
            }

            var bytes = File.ReadAllBytes(files[0]);

            _texture = new Texture2D(2, 2);
            _texture.LoadImage(bytes);
            _texture.Apply();

            _loadImageButton.setText($"\"{Path.GetFileName(files[0])}\"");

            updateDisplayedImage();
        }

        private void updateDisplayedImage()
        {
            createBackgroundImageIfNull();

            _backgroundImage.image().texture = _texture;
            _backgroundImage.image().color = Color.white;
        }

        private void removeDisplayedImage()
        {
            createBackgroundImageIfNull();

            _loadImageButton.setText($"Load Image");

            _backgroundImage.image().texture = null;
            _backgroundImage.image().color = Color.clear;
        }

        private void createBackgroundImageIfNull()
        {
            if (_backgroundImage == null)
            {
                _backgroundImage = UiFactory.createGenericUiElement("Overlay Image", transform.parent);
                _backgroundImage.setAnchors(Anchors.FillParent);
                _backgroundImage.moveToBack();

                var group = _backgroundImage.use<CanvasGroup>();
                group.alpha = 0f;
                group.interactable = false;
                group.blocksRaycasts = false;
            }
        }

        #endregion
    }
}
