using rose.row.data;
using rose.row.easy_package.audio;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.spawn;
using rose.row.util;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.ui.ingame.ingame_displayables.displayables
{
    public class VehicleDisplayableUiWidget : DisplayableUiWidget,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private bool _hovered;
        private UiElement _hoveredBorder;

        public override bool isCanvasGroupInteractable => true;

        public override void build()
        {
            base.build();

            _hoveredBorder = UiFactory.createGenericUiElement(
                name: "Hovered Border",
                element: this
            );
            _hoveredBorder.setBackground(ImageRegistry.capturePointSelected.get());
            _hoveredBorder.setAnchors(Anchors.FillParent);
            _hoveredBorder.setOffset(-4f, -4f, 4f, 4f);
            _hoveredBorder.moveToBack();

            updateHoveredState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (displayable is VehicleDisplayable vehicleDisplayable)
                Spawn.spawn(vehicleDisplayable.vehicle);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovered = true;
            Audio.play(AudioRegistry.mouse_hover.get());
            updateHoveredState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovered = false;
            updateHoveredState();
        }

        private void updateHoveredState()
        {
            _hoveredBorder.gameObject.SetActive(_hovered);
        }

        protected override void calculateAlpha(Camera playerCamera)
        {
            base.calculateAlpha(playerCamera);
            var group = use<CanvasGroup>();
            var interactable = group.alpha != 0f;
            group.interactable = interactable;
            group.blocksRaycasts = interactable;
        }
    }

    public class VehicleDisplayable : AbstractDisplayable
    {
        #region vehicle

        private VehicleSpawner.VehicleSpawnType _vehicleSpawnType;
        public VehicleSpawner.VehicleSpawnType vehicleSpawnType => _vehicleSpawnType;

        private Vehicle _vehicle;
        public Vehicle vehicle => _vehicle;

        public void setVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
            _vehicleSpawnType = _vehicle.spawnType();
        }

        #endregion

        #region spawning/destroying behaviour

        private void Start()
        {
            Debug.Log($"Vehicle spawned. Registering it in displayable widgets.");
            DisplayableUi.displayables?.Add(this);
            DisplayableUi.refreshDisplayableWidgets();
        }

        private void OnDestroy()
        {
            if (!DisplayableUi.displayables.Contains(this))
                return;

            Debug.Log($"Vehicle destroyed. Removing it from displayable widgets.");
            DisplayableUi.displayables?.Remove(this);
            DisplayableUi.refreshDisplayableWidgets();
        }

        #endregion

        #region ui display

        public override Texture2D icon => vehicleSpawnType.icon();
        public override Type displayableUiWidgetClass => typeof(VehicleDisplayableUiWidget);

        public override float getAlpha(Camera playerCamera)
        {
            if (LocalPlayer.actor == null || !LocalPlayer.actor.dead)
                return 0f;

            if (!vehicle.HasDriver() || vehicle.Driver().team != LocalPlayer.actor.team)
                return 0f;

            if (vehicle.getEmptyOrAiSeat(false) == null)
                return 0f;

            return 1f;
        }

        #endregion
    }
}
