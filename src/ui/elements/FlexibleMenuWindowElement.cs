using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using UnityEngine;

namespace rose.row.ui.elements
{
    public class FlexibleMenuWindowElement : UiElement
    {
        public const float k_headerHeight = 20f;
        public const float k_footerHeight = 12f;

        public float height = 400f;
        public float width = 310f;
        public new string name = "Menu";

        public Texture2D top => ImageRegistry.menuWindowTop.get();
        public Texture2D inside => ImageRegistry.menuWindowInside.get();
        public Texture2D bottom => ImageRegistry.menuWindowBottom.get();

        public UiElement wrapper;
        public UiElement wrapperHider;
        public UiElement header;
        public TextElement headerTitle;
        public UiElement footer;

        public override void build()
        {
            wrapper = UiFactory.createGenericUiElement("Wrapper", rectTransform);
            wrapper.image().texture = inside;
            wrapper.setSize(width, height);

            wrapperHider = UiFactory.createGenericUiElement("Wrapper Hider", wrapper.rectTransform);
            wrapperHider.image().color = new Color32(34, 34, 34, 127);
            wrapperHider.setAnchors(Anchors.FillParent);

            header = UiFactory.createGenericUiElement("Header", rectTransform);
            header.image().texture = top;
            header.setSize(width, k_headerHeight);
            header.setAnchoredPosition(0f, height / 2 + k_headerHeight / 2);

            headerTitle = UiFactory.createUiElement<TextElement>("Header Title", header.rectTransform);
            headerTitle.setText(name);
            headerTitle.setAnchors(Anchors.FillParent);
            headerTitle.setTextAlign(TMPro.HorizontalAlignmentOptions.Geometry);
            headerTitle.setTextAlign(TMPro.VerticalAlignmentOptions.Geometry);
            headerTitle.setFontSize(14);
            headerTitle.setFont(Fonts.defaultFont);
            headerTitle.setColor(new Color(0.6f, 0.6f, 0.6f));

            footer = UiFactory.createGenericUiElement("Footer", rectTransform);
            footer.image().texture = bottom;
            footer.setSize(width, k_footerHeight);
            footer.setAnchoredPosition(0f, -height / 2 - k_footerHeight / 2);
        }

        public void clean()
        {
            Destroy(wrapper.gameObject);
            Destroy(header.gameObject);
            Destroy(footer.gameObject);
        }

        public void rebuild()
        {
            clean();
            build();
        }
    }
}