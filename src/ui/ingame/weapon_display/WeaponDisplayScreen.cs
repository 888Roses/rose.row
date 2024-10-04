using rose.row.data;
using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace rose.row.ui.ingame.weapon_display
{
    public class WeaponDisplayWeaponElement : UiElement
    {
        public const float k_NormalWidth = 96;
        public const float k_NormalHeight = 48;
        public const float k_HighlightWidth = 120;
        public const float k_HighlightHeight = 60;

        public int slot;

        public CanvasGroup group;

        public UiElement background;
        public UiElement imageBackground;
        public UiElement border;

        public TextElement key;

        private bool _isActive;
        public bool isActive => _isActive;

        public override void build()
        {
            group = use<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            background = UiFactory.createGenericUiElement("Background", this);
            background.setAnchors(Anchors.FillParent);
            background.image().color = new Color32(28, 28, 28, 125);

            imageBackground = UiFactory.createGenericUiElement("Image", this);
            imageBackground.setAnchors(Anchors.MiddleCenter);
            imageBackground.image().color = Color.clear;

            border = UiFactory.createGenericUiElement("Border", this);
            border.setAnchors(Anchors.FillParent);
            border.image().texture = ImageRegistry.weaponDisplayBorder.get();

            key = UiFactory.createUiElement<TextElement>("Key Text", this);
            key.build();
            key.setFontSize(38f);
            key.setFont(Fonts.defaultFont);
            key.setShadow(1, Color.black.with(a: 0.5f));

            key.setTextAlign(HorizontalAlignmentOptions.Geometry, VerticalAlignmentOptions.Geometry);
            key.setAnchors(Anchors.TopRight);
            key.setPivot(1, 1);
            key.setSize(16, 32);
            key.setAnchoredPosition(-8, -4);

            setActive(false);
        }

        public void setWeapon(WeaponManager.WeaponEntry weapon)
        {
            if (weapon == null || weapon.uiSprite == null)
            {
                imageBackground.image().color = Color.clear;
                Debug.LogWarning($"Tried assigning a null weapon entry to a slot. This is not allowed!");
                return;
            }

            imageBackground.image().color = Color.white;
            imageBackground.image().texture = weapon.uiSprite.texture;
            updateImageSize();
        }

        public void updateImageSize()
        {
            if (imageBackground.image().texture == null)
                return;

            var tex = imageBackground.image().texture;
            var ratio = getHeight() * tex.width / tex.height;
            imageBackground.setSize(ratio, getHeight());
        }

        public void setActive(bool active)
        {
            _isActive = active;
            updateActiveState();
        }

        private void updateActiveState()
        {
            border.image().color = _isActive ? Color.white : Color.black;
            key.setColor(_isActive ? "#CDA04B" : "#D7D7D7");
            group.alpha = _isActive ? 1 : 0.5f;

            if (_isActive)
            {
                setSize(k_HighlightWidth, k_HighlightHeight);
                key.setAnchoredPosition(-8, -12);
            }
            else
            {
                setSize(k_NormalWidth, k_NormalHeight);
                key.setAnchoredPosition(-8, -4);
            }

            updateImageSize();
        }

        public void updateText()
        {
            var inputName = $"Weapon{slot + 1}";
            var inputValue = (SteelInput.KeyBinds) Enum.Parse(typeof(SteelInput.KeyBinds), inputName, true);
            var text = SteelInput.GetInput(inputValue).PositiveLabel().Trim();
            key.setText(text[text.Length - 1].ToString());
            key.setColor(_isActive ? "#CDA04B" : "#D7D7D7");
        }
    }

    public class WeaponDisplayElement : UiElement
    {
        public const float k_Gap = 33;

        private UiElement _container;

        public WeaponDisplayWeaponElement[] weapons;
        public WeaponDisplayWeaponElement weapon1;
        public WeaponDisplayWeaponElement weapon2;
        public WeaponDisplayWeaponElement weapon3;
        public WeaponDisplayWeaponElement weapon4;
        public WeaponDisplayWeaponElement weapon5;

        public override void build()
        {
            setAnchors(Anchors.FillParent);

            _container = UiFactory.createGenericUiElement("Container", this);
            _container.setAnchors(Anchors.BottomCenter, true, true);
            _container.setSize(0, 0);
            _container.setAnchoredPosition(0, 20);

            weapons = new WeaponDisplayWeaponElement[5]
            {
                weapon1 = createWeapon(0, true),
                weapon2 = createWeapon(1, false),
                weapon3 = createWeapon(2, false),
                weapon4 = createWeapon(3, false),
                weapon5 = createWeapon(4, false),
            };

            weapon4.gameObject.SetActive(false);
            weapon5.gameObject.SetActive(false);

            updatePositions();
        }

        public WeaponDisplayWeaponElement createWeapon(int slot, bool selected)
        {
            var element = UiFactory.createUiElement<WeaponDisplayWeaponElement>($"Weapon Element {slot}", _container);
            element.updateText();
            element.setAnchors(Anchors.BottomCenter, true, true);
            element.setActive(selected);

            return element;
        }

        public void updatePositions()
        {
            // Selects only weapon slots that are enabled so that we don't calculate the offset between slots while taking
            // into account invisible ones.
            var activeWeaponSlots = weapons.Where(x => x.gameObject.activeSelf).ToArray();
            // The full width of all the elements put next to one another.
            var elementWidth = activeWeaponSlots.Length * WeaponDisplayWeaponElement.k_NormalWidth;
            // The full width of all gaps between elements, put next to one another.
            var elementGapWidth = activeWeaponSlots.Length * k_Gap;
            var totalWidth = elementWidth + elementGapWidth;
            var increment = totalWidth / activeWeaponSlots.Length;
            for (int i = 0; i < activeWeaponSlots.Length; i++)
            {
                var currentIncrement = increment * i;
                var pos = currentIncrement - totalWidth / 2f + (WeaponDisplayWeaponElement.k_NormalWidth + k_Gap) / 2;
                activeWeaponSlots[i].setAnchoredPosition(pos, 0);
            }
        }

        public void updateWeapons(Weapon[] weapons, int selectedSlot)
        {
            foreach (var slot in this.weapons)
                slot.gameObject.SetActive(false);

            for (int i = 0; i < weapons.Length; i++)
            {
                Weapon weapon = weapons[i];
                var slot = this.weapons[i];
                slot.setWeapon(weapon == null ? null : weapon.weaponEntry);
                slot.gameObject.SetActive(weapon != null);

                if (weapon == null)
                    continue;

                slot.setActive(selectedSlot == weapon.slot);
            }

            updatePositions();
        }
    }

    public class WeaponDisplayScreen : Singleton<WeaponDisplayScreen>
    {
        public static void create()
        {
            var gameObject = new GameObject("Weapon Display");
            gameObject.AddComponent<WeaponDisplayScreen>();
        }

        private UiScreen _screen;
        private WeaponDisplayElement _element;

        private void Awake()
        {
            build();
        }

        private void build()
        {
            _screen = UiFactory.createUiScreen(name: "Screen", order: ScreenOrder.weaponDisplay, parent: transform);
            _element = UiFactory.createUiElement<WeaponDisplayElement>(name: "WeaponDisplayElement", screen: _screen);
        }

        public void updateWeaponItems()
        {
            _element.updateWeapons(FpsActorController.instance.actor.weapons, FpsActorController.instance.actor.activeWeapon.slot);
        }
    }
}
