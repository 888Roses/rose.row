using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory.elements
{
    public class ScrollableElement : UiElement
    {
        private UiElement _container;
        private ScrollRect _scrollRect;

        private UiElement _content;
        private ContentSizeFitter _contentSizeFitter;

        public UiElement container => _container;
        public UiElement content => _content;
        public ScrollRect scrollRect => _scrollRect;
        public ContentSizeFitter contentSizeFitter => _contentSizeFitter;

        protected override void Awake()
        {
        }

        public override void build()
        {
            _container = UiFactory.createGenericUiElement("Scroll Rect", this);
            _container.setAnchors(Anchors.FillParent);
            _scrollRect = _container.use<ScrollRect>();
            _scrollRect.scrollSensitivity = 10f;
            _scrollRect.movementType = ScrollRect.MovementType.Clamped;
        }

        public void setContent(UiElement content)
        {
            _content = content;
            _content.setPivot(0.5f, 1);
            _content.setParent(_container);
            _content.setAnchoredPosition(0, 0);

            _contentSizeFitter = content.use<ContentSizeFitter>();
            _contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            _scrollRect.content = content.rectTransform;
        }
    }
}