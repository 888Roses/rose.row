using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static WeaponManager;

namespace rose.row.dev.dev_weapon_selec
{
    public class WeaponEntryElement : UiElement, IPointerDownHandler
    {
        public WeaponEntry entry;

        private UiElement _background;
        private UiElement _uiSprite;
        private TextElement _text;

        protected override void Awake()
        { }

        public override void build()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);
            _background.image().color = new Color32(22, 22, 22, 255);

            _uiSprite = UiFactory.createGenericUiElement("Picture", this);
            _uiSprite.setAnchors(Anchors.MiddleCenter);
            if (entry.uiSprite != null)
            {
                var tex = entry.uiSprite.texture;
                _uiSprite.image().texture = tex;
                _uiSprite.setSize(getWidth(), tex.height * getWidth() / tex.width);
            }

            _text = UiFactory.createUiElement<TextElement>("Text", this);
            _text.build();
            _text.setPivot(0.5f, 0);
            _text.setAnchors(Anchors.BottomCenter);
            _text.setHeight(38f);
            _text.setFont(Fonts.defaultFont);
            _text.setColor(Color.white);
            _text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _text.setTextAlign(VerticalAlignmentOptions.Geometry);
            _text.setFontSize(16);
            _text.setText(entry.name);
        }

        private bool setEntry(WeaponEntry entry)
        {
            if (LoadoutUi.instance == null)
                return false;

            var player = FpsActorController.instance;
            if (player == null || player.actor.dead)
                return false;

            var playerActor = player.actor;
            playerActor.EquipNewWeaponEntry(entry, playerActor.activeWeapon.slot, true);
            var slot = playerActor.activeWeapon.slot;

            switch (slot)
            {
                case 0:
                    LoadoutUi.instance.loadout.primary = entry;
                    break;

                case 1:
                    LoadoutUi.instance.loadout.secondary = entry;
                    break;

                case 2:
                    LoadoutUi.instance.loadout.gear1 = entry;
                    break;

                case 3:
                    LoadoutUi.instance.loadout.gear2 = entry;
                    break;

                case 4:
                    LoadoutUi.instance.loadout.gear3 = entry;
                    break;
            }

            // private void SaveDefaultSlotEntry(int slot, WeaponManager.WeaponEntry entry)
            LoadoutUi.instance.executePrivate("SaveDefaultSlotEntry", slot, entry);

            return true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (setEntry(entry))
                WeaponSelectionScreen.instance.setEnabled(false);
        }
    }
}