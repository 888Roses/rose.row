using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using UnityEngine;

namespace rose.row.main_menu.ui.login.elements
{
    /// <summary>
    /// An element that loads in a random image as a background each time it is spawned.
    /// </summary>
    public class RandomImageBackground : UiElement
    {
        public Texture2D[] backgrounds;
        public int currentBackgroundIndex;

        private UiElement _background;

        public void initialize(string[] backgroundNames)
        {
            var backgrounds = new Texture2D[backgroundNames.Length];
            for (var i = 0; i < backgroundNames.Length; i++)
                backgrounds[i] = ImageLoader.textures[backgroundNames[i]];

            initialize(backgrounds);
        }

        public void initialize(Texture2D[] backgrounds)
        {
            this.backgrounds = backgrounds;

            _background = UiFactory.createGenericUiElement("Background", this);
            _background.image();

            changeBackground();
        }

        public void changeBackground(int index = -1)
        {
            currentBackgroundIndex = index == -1
                ? Random.Range(0, backgrounds.Length)
                : index;

            currentBackgroundIndex = Mathf.Clamp(
                value: currentBackgroundIndex,
                min: 0,
                max: backgrounds.Length - 1
            );

            updateBackground();
        }

        private void Update()
        {
            _background.setSize(Screen.width, Screen.height);
        }

        private void updateBackground()
        {
            _background.image().texture = backgrounds[currentBackgroundIndex];
        }
    }
}