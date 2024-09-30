using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace rose.row.ui.ingame.screens.end_screen
{
    public class StarElement : UiElement, IPointerEnterHandler, IPointerExitHandler
    {
        public UiElement icon;
        public bool hovered;
        public Action onHovered;

        public override void build()
        {
            // This is only used so we can hover over the element without it having a graphic.
            image();
            use<Mask>().showMaskGraphic = false;

            icon = UiFactory.createGenericUiElement(name, this);
            icon.image().texture = ImageRegistry.endScreenStar.get();
            icon.setAnchors(Anchors.FillParent);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovered = true;
            onHovered?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hovered = false;
        }
    }
}
