using UnityEngine;
using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory.elements
{
    public abstract class AbstractLayoutGroupElement : UiElement
    {
        protected abstract LayoutGroup layoutGroup { get; }

        public void setPadding(RectOffset padding) => layoutGroup.padding = padding;

        public void setPadding(Vector2Int horizontal, Vector2Int vertical)
            => setPadding(new RectOffset(horizontal.x, horizontal.y, vertical.x, vertical.y));

        public void setPadding(int horizontal, int vertical)
            => setPadding(new RectOffset(horizontal, horizontal, vertical, vertical));

        public void setPadding(int padding)
            => setPadding(new RectOffset(padding, padding, padding, padding));
    }
}