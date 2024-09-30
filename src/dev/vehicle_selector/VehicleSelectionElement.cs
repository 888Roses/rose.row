using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.rendering.game_preview;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.dev.vehicle_selector
{
    public class VehicleSelectionElement : UiElement
    {
        private UiElement _background;
        private GridElement _grid;
        private ScrollableElement _scrollable;
        private List<VehicleEntryUiElement> _vehicleUiElements;

        protected override void Awake() { }

        #region building

        public override void build()
        {
            createBackground();
            createScrollable();
            createGrid();
            createVehicleEntries();
        }

        private void createBackground()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);
            _background.image().color = new Color32(17, 17, 17, 255);
        }

        private void createScrollable()
        {
            _scrollable = UiFactory.createUiElement<ScrollableElement>("Scrollable", _background);
            _scrollable.build();
            _scrollable.setAnchors(Anchors.FillParent);

            _scrollable.container.image();
            _scrollable.container.use<Mask>().showMaskGraphic = false;
        }

        private void createGrid()
        {
            _grid = UiFactory.createUiElement<GridElement>("Grid Layout", _scrollable.container);
            _grid.build();
            _grid.setAnchors(Anchors.StretchTop);
            _grid.setPivot(0.5f, 1f);
            _grid.setHeight(400f);
            var padding = 2 * 8 - 4 * 4;
            _grid.setTileSize(Screen.width / 4f - padding, Screen.height / 4f - padding);
            _grid.setSpacing(4);
            _grid.setPadding(8);
            _scrollable.setContent(_grid);
        }

        #endregion

        private void createVehicleEntries()
        {
            _vehicleUiElements = new List<VehicleEntryUiElement>();

            foreach (var vehicle in VehiclePreviewManager.instance.pairs)
            {
                var entry = createVehicleEntry(vehicle);
                if (entry == null)
                {
                    Debug.LogError($"Could not create entry for vehicle because it's prefab was null.");
                    continue;
                }

                _vehicleUiElements.Add(entry);
            }

            LayoutRebuilder.MarkLayoutForRebuild(_grid.rectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(_scrollable.rectTransform);
        }

        private VehicleEntryUiElement createVehicleEntry(VehicleInfoPair info)
        {
            if (info.info.prefab == null)
                return null;

            var vehicle = UiFactory.createUiElement<VehicleEntryUiElement>(info.info.prefab.transform.name, _grid);
            vehicle.prefab = info.info.prefab;
            vehicle.texture = info.texture;
            vehicle.build();

            return vehicle;
        }
    }
}
