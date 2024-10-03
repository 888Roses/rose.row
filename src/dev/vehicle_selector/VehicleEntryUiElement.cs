using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.dev.vehicle_selector
{
    public class VehicleEntryUiElement : UiElement, IPointerClickHandler
    {
        public Texture2D texture;
        public GameObject prefab;
        public Action onClick;

        protected override void Awake()
        { }

        private UiElement _background;
        private UiElement _uiSprite;
        private TextElement _text;

        public override void build()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);
            _background.image().color = new Color32(22, 22, 22, 255);

            _uiSprite = UiFactory.createGenericUiElement("Picture", this);
            _uiSprite.setAnchors(Anchors.FillParent);
            if (texture != null)
                _uiSprite.image().texture = texture;

            _text = UiFactory.createUiElement<TextElement>("Text", this);
            _text.build();
            _text.setPivot(0.5f, 0);
            _text.setAnchors(Anchors.StretchBottom);
            _text.setHeight(38f);
            _text.setFont(Fonts.defaultFont);
            _text.setColor(Color.white);
            _text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _text.setTextAlign(VerticalAlignmentOptions.Geometry);
            _text.setFontSize(16);
            _text.setText(prefab.name);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();

            var player = FpsActorController.instance;
            if (player == null || player.dead())
                return;

            var camera = PlayerFpParent.instance.fpCamera.transform;
            var created = Instantiate(prefab).transform;
            var maxBounds = created.getMaxBounds();
            maxBounds.gizmoDrawEdges(Color.green, 3f);
            created.transform.position = camera.position + camera.forward * Mathf.Max(maxBounds.size.x, maxBounds.size.z) + Vector3.up * Mathf.Max(2, maxBounds.size.y);
            created.transform.forward = camera.forward;

            VehicleSelectionScreen.instance.setEnabled(false);
        }
    }
}