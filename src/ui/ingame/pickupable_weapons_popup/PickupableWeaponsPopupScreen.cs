using rose.row.actor.player;
using rose.row.data;
using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.ui.ingame.pickupable_weapons_popup
{
    public class PickupableWeaponsPopupElement : UiElement
    {
        public const int k_Gap = 4;

        public UiElement container;
        public TextElement text;

        public UiElement keyContainer;
        public UiElement keyContainerPadding;
        public TextElement key;

        public CanvasGroup group;

        public override void build()
        {
            setAnchors(Anchors.FillParent);

            container = UiFactory.createGenericUiElement(name: "Container", element: this);
            container.setHeight(40);
            container.setAnchoredPosition(2f, -relativeHeight(152f));
            group = container.use<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            text = UiFactory.createUiElement<TextElement>(name: "Weapon Name", element: container);
            text.build();
            text.setAnchors(Anchors.StretchRight, true, true);
            text.setPivot(1, 0.5f);

            text.setColor("#AEA296");
            text.setFontSize(26);
            text.setFont(Fonts.defaultFont);
            text.setTextAlign(VerticalAlignmentOptions.Geometry);

            text.setShadow(1, Color.black);

            keyContainer = UiFactory.createGenericUiElement(name: "Key Container", element: container);
            keyContainer.setAnchors(Anchors.StretchLeft, true, true);
            keyContainer.setPivot(0, 0.5f);
            var fitter = keyContainer.use<AspectRatioFitter>();
            fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            fitter.aspectRatio = 1;
            keyContainer.image().color = new Color32(122, 118, 112, 255);

            keyContainerPadding = UiFactory.createGenericUiElement(name: "Padding", element: keyContainer);
            keyContainerPadding.setAnchors(Anchors.FillParent);
            keyContainerPadding.setOffset(0, 0, -4f, 0);

            key = UiFactory.createUiElement<TextElement>(name: "Key", element: keyContainerPadding);
            key.build();
            key.setAnchors(Anchors.FillParent);
            key.setColor("#AEA296");
            key.setFontSize(36);
            key.setFont(Fonts.defaultFont);
            key.setTextAlign(VerticalAlignmentOptions.Geometry, HorizontalAlignmentOptions.Geometry);
            key.setShadow(1, Color.black);
            key.setText("F");

            setWeapon(null);
        }

        public void setWeapon(Weapon weapon)
        {
            if (weapon == null)
            {
                group.alpha = 0f;
                text.setText($"OOPSIE URE NOT SUPPOSED TO BE SEEING THIS TIHEE");
                return;
            }

            text.setText(weapon.displayName);
            text.setWidth(text.text.preferredWidth);
            container.setWidth(text.text.preferredWidth + k_Gap + container.getHeight());
            group.alpha = 1f;
        }
    }

    public class PickupableWeaponsPopupScreen : Singleton<PickupableWeaponsPopupScreen>
    {
        public static void create()
        {
            var gameObject = new GameObject("Pickupable Weapons Popup");
            gameObject.AddComponent<PickupableWeaponsPopupScreen>();
        }

        private UiScreen _screen;
        private PickupableWeaponsPopupElement _element;

        private void Awake()
        {
            build();
        }

        private void build()
        {
            _screen = UiFactory.createUiScreen(name: "Screen", order: ScreenOrder.weaponPickupPopup, parent: transform);
            _element = UiFactory.createUiElement<PickupableWeaponsPopupElement>(name: "Popup", screen: _screen);
        }

        private void Update()
        {
            var currentWatchedPickupableWeapon = PlayerPickupWeapons.currentlyWatchedPickupableWeapon;
            _element.setWeapon(currentWatchedPickupableWeapon == null ? null : currentWatchedPickupableWeapon.weapon);
        }
    }
}
