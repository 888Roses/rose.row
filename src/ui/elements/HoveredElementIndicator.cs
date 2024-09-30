using rose.row.easy_package.ui.factory.elements;
using UnityEngine;

namespace rose.row.ui.elements
{
    public class HoveredElementIndicator : UiElement
    {
        public float offset = 4f;

        public override void build()
        {
            setAnchors(Anchors.FillParent);
            setOffset(Vector2.one * -offset, Vector2.one * offset);
            moveToBack();
            image();
        }
    }
}