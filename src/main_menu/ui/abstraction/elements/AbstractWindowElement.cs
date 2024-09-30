using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;

namespace rose.row.main_menu.ui.abstraction.elements
{
    /// <summary>
    /// An abstract window element that can be extended to create any
    /// kind of floating window.
    /// </summary>
    public abstract class AbstractWindowElement : UiElement
    {
        public abstract float anchorX { get; }
        public abstract float anchorY { get; }

        /// <summary>
        /// The anchors of the the <see cref="LoginWindowElement"/>.
        /// </summary>
        public virtual LiteralAnchors anchors => new LiteralAnchors(
            anchorX, anchorY, 1f - anchorX, 1f - anchorY
        );

        #region components

        /// <summary>
        /// The background wrapper element of that window. While the window englobes
        /// the entire screen, this element will actually have the right dimensions
        /// specified by the <see cref="anchors"/>.
        /// </summary>
        protected UiElement _wrapper;

        #endregion components

        protected override void Awake()
        {
            // Overriding "Awake" so this.build() is not called immediately
            // when the object is created.
        }

        /// <summary>
        /// Builds all the components needed for the UI.
        /// </summary>
        public override void build()
        {
            #region assign initial style properties

            // Assign base style properties to this very element.

            setAnchors(Anchors.FillParent);
            setAnchoredPosition(0, 0);

            #endregion assign initial style properties

            createWrapper();
        }

        #region create components

        /// <summary>
        /// A method responsible for creating the <see cref="_wrapper"/> element.
        /// </summary>
        protected virtual void createWrapper()
        {
            _wrapper = UiFactory.createGenericUiElement("Wrapper", this);
            _wrapper.setAnchors(anchors);
        }

        #endregion create components
    }
}