using UnityEngine;

namespace rose.row.easy_package.ui.factory.elements
{
    public class ProgressBarElement : UiElement
    {
        public UiElement background;
        public UiElement fill;
        public UiElement bar;

        public float progress;

        public override void build()
        {
            background = UiFactory.createGenericUiElement("Background", this);
            background.setAnchors(Anchors.FillParent);
            background.image().color = new Color32(131, 130, 123, 255);

            fill = UiFactory.createGenericUiElement("Background", this);
            fill.setAnchors(Anchors.FillParent);
            fill.image().color = new Color32(51, 107, 153, 255);

            bar = UiFactory.createGenericUiElement("Bar", fill);
            bar.setAnchors(Anchors.StretchRight);
            bar.setPivot(1, 0.5f);
            bar.setWidth(1);
            bar.setAnchoredPosition(0, 0);
            bar.image().color = Color.white;
        }

        public void setProgress(float progress)
        {
            this.progress = progress;
            fill.setAnchors(new LiteralAnchors(0, 0, progress, 1));
        }
    }
}
