using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory.elements
{
    public class VerticalListElement : AbstractLayoutGroupElement
    {
        private VerticalLayoutGroup _list;
        public VerticalLayoutGroup list => _list;

        protected override void Awake()
        {
        }

        public override void build()
        {
            _list = use<VerticalLayoutGroup>();

            setChildControlAll(false);
            setChildForceExpandAll(false);
            setChildScaleAll(false);
        }

        protected override LayoutGroup layoutGroup => _list;

        public void setSpacing(float spacing) => list.spacing = spacing;

        public void setChildForceExpandWidth(bool childForceExpandWidth)
            => list.childForceExpandWidth = childForceExpandWidth;

        public void setChildForceExpandHeight(bool childForceExpandHeight)
            => list.childForceExpandHeight = childForceExpandHeight;

        public void setChildForceExpandAll(bool childForceExpandAll)
        {
            setChildForceExpandWidth(childForceExpandAll);
            setChildForceExpandHeight(childForceExpandAll);
        }

        public void setChildControlWidth(bool childControlWidth)
            => list.childControlWidth = childControlWidth;

        public void setChildControlHeight(bool childControlHeight)
            => list.childControlHeight = childControlHeight;

        public void setChildControlAll(bool childControlAll)
        {
            setChildControlWidth(childControlAll);
            setChildControlHeight(childControlAll);
        }

        public void setChildScaleWidth(bool childScaleWidth)
            => list.childScaleWidth = childScaleWidth;

        public void setChildScaleHeight(bool childScaleHeight)
            => list.childScaleHeight = childScaleHeight;

        public void setChildScaleAll(bool childScaleAll)
        {
            setChildScaleWidth(childScaleAll);
            setChildScaleHeight(childScaleAll);
        }

        public void setReverseArrangement(bool reverseArrangement)
        {
            list.reverseArrangement = reverseArrangement;
        }
    }
}