using System.Collections.Generic;

namespace rose.row.easy_package.ui.factory.elements
{
    public class FlexibleListElement : UiElement
    {
        public bool expandItemsHorizontal = true;
        public float itemGap = 4f;
        public int maxItems = -1;

        public UiElement this[int index] => _children[index];

        private List<UiElement> _children;
        public List<UiElement> children => _children;

        protected override void Awake()
        {
            base.Awake();

            _children = new List<UiElement>();
        }

        public void addChild(UiElement child, bool recalculateLayout = true)
        {
            _children.Add(child);

            if (maxItems > 0)
            {
                while (_children.Count > maxItems)
                {
                    Destroy(_children[0].gameObject);
                    _children.RemoveAt(0);
                }
            }

            if (recalculateLayout)
                this.recalculateLayout();
        }

        public void addChildren(IEnumerable<UiElement> children, bool recalculateLayout = true)
        {
            _children.AddRange(children);

            if (maxItems > 0)
            {
                while (_children.Count > maxItems)
                {
                    Destroy(_children[0].gameObject);
                    _children.RemoveAt(0);
                }
            }

            if (recalculateLayout)
                this.recalculateLayout();
        }

        public void removeChild(UiElement child, bool recalculateLayout = true)
        {
            Destroy(child.gameObject);
            _children.Remove(child);

            if (recalculateLayout)
                this.recalculateLayout();
        }

        public void removeChild(int childIndex, bool recalculateLayout = true)
        {
            Destroy(_children[childIndex]);
            _children.RemoveAt(childIndex);

            if (recalculateLayout)
                this.recalculateLayout();
        }

        public void recalculateLayout()
        {
            var stackHeight = 0f;

            foreach (var item in _children)
            {
                if (expandItemsHorizontal)
                {
                    var previousHeight = item.getHeight();
                    item.setAnchors(Anchors.StretchTop, updateOffsets: true);
                    item.setHeight(previousHeight);
                }
                else
                {
                    item.setAnchors(Anchors.TopCenter, updateOffsets: false);
                }

                item.setPivot(0, 1);
                item.setAnchoredPosition(0, stackHeight);

                stackHeight -= item.getHeight() + itemGap;
            }
        }
    }
}