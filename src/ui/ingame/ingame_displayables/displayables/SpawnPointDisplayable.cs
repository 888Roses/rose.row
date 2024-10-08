using rose.row.data;
using rose.row.easy_events;
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
    public class SpawnPointDisplayableUiWidget : DisplayableUiWidget,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private bool _hovered;

        private UiElement _hoveredBorder;
        private UiElement _ownerBackground;
        private UiElement _ownerIcon;

        public override bool isCanvasGroupInteractable => true;

        private void OnDestroy()
        {
            Events.onSpawnPointCaptured.after -= onPointCaptured;
        }

        private void onPointCaptured(SpawnPoint point, int team, bool isInitialOwner)
        {
            if (displayable is SpawnPointDisplayable spawnPointDisplayable)
                if (point == spawnPointDisplayable.spawnPoint)
                    refreshUi();
        }

        public override void build()
        {
            base.build();
            Events.onSpawnPointCaptured.after += onPointCaptured;

            _hoveredBorder = UiFactory.createGenericUiElement(
                name: "Hovered Border",
                element: this
            );
            _hoveredBorder.setBackground(ImageRegistry.capturePointSelected.get());
            _hoveredBorder.setAnchors(Anchors.FillParent);
            _hoveredBorder.setOffset(-4f, -4f, 4f, 4f);
            _hoveredBorder.moveToBack();

            buildOwnerUi();

            updateHoveredState();
            refreshUi();
        }

        private void refreshUi()
        {
            if (displayable is SpawnPointDisplayable spawnPointDisplayable)
            {
                setBackground(spawnPointDisplayable.icon);
                _ownerBackground.setBackground(spawnPointDisplayable.factionContainerBackground);

                var spawnPoint = spawnPointDisplayable.spawnPoint;
                var faction = spawnPoint.owningFaction();
                if (faction == null)
                {
                    _ownerIcon.setBackgroundEnabled(false);
                }
                else
                {
                    _ownerIcon.setBackgroundEnabled(true);
                    _ownerIcon.setBackground(faction.factionIcon);
                }
            }
        }

        private void buildOwnerUi()
        {
            _ownerBackground = UiFactory.createGenericUiElement("Owner Background", this);
            _ownerBackground.setAnchors(Anchors.TopLeft);
            _ownerBackground.setPivot(new Vector2(0, 1));
            var backgroundSize = Vector2.one * 32f;
            _ownerBackground.setSize(backgroundSize);
            _ownerBackground.setAnchoredPosition(new Vector2(-backgroundSize.x / 3f, backgroundSize.y / 3f));
            _ownerBackground.image();

            _ownerIcon = UiFactory.createGenericUiElement("Icon", _ownerBackground);
            _ownerIcon.setAnchors(Anchors.FillParent);
            var ownerIconOffset = backgroundSize.x / 4 + 1;
            _ownerIcon.setOffset(ownerIconOffset, ownerIconOffset, -ownerIconOffset, -ownerIconOffset);
            _ownerIcon.image();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (displayable is SpawnPointDisplayable spawnPointDisplayable)
            {
                if (!spawnPointDisplayable.spawnPoint.isAlly())
                    return;

                MinimapUi.SelectSpawnPoint(spawnPointDisplayable.spawnPoint);
                Spawn.spawn(null);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (displayable is SpawnPointDisplayable spawnPointDisplayable)
            {
                if (!spawnPointDisplayable.spawnPoint.isAlly())
                    return;
            }

            _hovered = true;
            Audio.play(AudioRegistry.mouseHover.get());
            updateHoveredState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (displayable is SpawnPointDisplayable spawnPointDisplayable)
            {
                if (!spawnPointDisplayable.spawnPoint.isAlly())
                    return;
            }

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
            var interactable = group.alpha > 0.75f;
            group.interactable = interactable;
            group.blocksRaycasts = interactable;
        }
    }

    public class SpawnPointDisplayable : AbstractDisplayable
    {
        #region spawnPoint

        private SpawnPoint _spawnPoint;
        public SpawnPoint spawnPoint => _spawnPoint;

        public void setSpawnPoint(SpawnPoint spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        #endregion

        #region ui display

        public override Texture2D icon => spawnPoint.getCapturePointBackground();
        public Texture2D factionContainerBackground => spawnPoint.getSpawnPointBackground();
        public override Type displayableUiWidgetClass => typeof(SpawnPointDisplayableUiWidget);

        public override float getAlpha(Camera playerCamera)
        {
            if (LocalPlayer.actor == null || !LocalPlayer.actor.dead)
                return 0f;

            if (spawnPoint.isEnemy())
                return 0.75f;

            return 1f;
        }

        #endregion
    }
}
