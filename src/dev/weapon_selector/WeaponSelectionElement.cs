using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using UnityEngine;
using UnityEngine.UI;
using static WeaponManager;

namespace rose.row.dev.dev_weapon_selec
{
    public class WeaponSelectionElement : UiElement
    {
        private UiElement _background;
        private GridElement _grid;
        private ScrollableElement _scrollable;

        protected override void Awake()
        { }

        public override void build()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);
            _background.image().color = new Color32(17, 17, 17, 255);

            _scrollable = UiFactory.createUiElement<ScrollableElement>("Scrollable", _background);
            _scrollable.build();
            _scrollable.setAnchors(Anchors.FillParent);

            _scrollable.container.image();
            _scrollable.container.use<Mask>().showMaskGraphic = false;

            _grid = UiFactory.createUiElement<GridElement>("Grid Layout", _scrollable.container);
            _grid.build();
            _grid.setAnchors(Anchors.StretchTop);
            _grid.setPivot(0.5f, 1f);
            _grid.setHeight(400f);
            _grid.setTileSize(Screen.width / 8f, Screen.height / 8f);
            _grid.setSpacing(4);
            _grid.setPadding(8);
            _scrollable.setContent(_grid);

            foreach (var weapon in instance.allWeapons)
                createWeaponEntry(weapon);
        }

        private void createWeaponEntry(WeaponEntry entry)
        {
            var weapon = UiFactory.createUiElement<WeaponEntryElement>(entry.name, _grid);
            weapon.entry = entry;
            weapon.build();

            LayoutRebuilder.MarkLayoutForRebuild(_grid.rectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(_scrollable.rectTransform);
        }
    }
}