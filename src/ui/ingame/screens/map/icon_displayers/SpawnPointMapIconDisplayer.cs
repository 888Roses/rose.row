using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.elements;
using rose.row.util;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.ui.ingame.screens.map.icon_displayers
{
    public class SpawnPointMapIconDisplayer : AbstractTargetMapIconDisplayer
    {
        public SpawnPoint spawnPoint;

        public void setup(SpawnPoint spawnPoint)
        {
            this.spawnPoint = spawnPoint;
            //buildUi();
        }

        public override Transform target => spawnPoint.transform;

        #region ui

        public UiElement capturePointBackground;
        public UiElement capturePointIcon;

        public UiElement ownerBackground;
        public UiElement ownerIcon;

        public HoveredElementIndicator hoveredIcon;

        protected virtual void buildCapturePointUi()
        {
            capturePointBackground = UiFactory.createGenericUiElement("Background", rectTransform);
            capturePointBackground.setAnchors(UiElement.Anchors.FillParent);
            capturePointBackground.image();

            capturePointIcon = UiFactory.createGenericUiElement("Icon", capturePointBackground.rectTransform);
            capturePointIcon.setAnchors(UiElement.Anchors.FillParent);
            capturePointIcon.image();
        }

        protected virtual void buildOwnerUi()
        {
            ownerBackground = UiFactory.createGenericUiElement("Owner Background", rectTransform);
            ownerBackground.setAnchors(UiElement.Anchors.TopLeft);
            ownerBackground.setPivot(new Vector2(0, 1));
            var backgroundSize = Vector2.one * 32f;
            ownerBackground.setSize(backgroundSize);
            ownerBackground.setAnchoredPosition(new Vector2(-backgroundSize.x / 3f, backgroundSize.y / 3f));
            ownerBackground.image();

            ownerIcon = UiFactory.createGenericUiElement("Icon", ownerBackground.rectTransform);
            ownerIcon.setAnchors(UiElement.Anchors.FillParent);
            var ownerIconOffset = backgroundSize.x / 4 + 1;
            ownerIcon.setOffset(ownerIconOffset, ownerIconOffset, -ownerIconOffset, -ownerIconOffset);
            ownerIcon.image();
        }

        protected virtual void buildHoveredUi()
        {
            hoveredIcon = UiFactory.createUiElement<HoveredElementIndicator>("Hover Indicator", rectTransform);
        }

        public override void buildUi()
        {
            buildCapturePointUi();
            buildOwnerUi();
            buildHoveredUi();

            initializeUi();
        }

        protected virtual void initializeUi()
        {
            if (hoveredIcon.image() == null)
                hoveredIcon.setImage(hoveredIcon.image());

            capturePointIcon.setBackgroundEnabled(false);
            hoveredIcon.image().texture = ImageRegistry.capturePointSelected.get();
            hoveredIcon.gameObject.SetActive(false);
        }

        public virtual void refreshUi()
        {
            return;

            capturePointBackground.image().texture = capturePointTextureFromTeam(spawnPoint.owner);
            ownerBackground.image().texture = spawnPointTextureFromTeam(spawnPoint.owner);

            refreshFactionIcon();
        }

        protected virtual void refreshFactionIcon()
        {
            var faction = spawnPoint.owningFaction();

            if (faction == null)
            {
                ownerIcon.setBackgroundEnabled(false);
            }
            else
            {
                ownerIcon.setBackgroundEnabled(true);
                ownerIcon.image().texture = faction.factionIcon;
            }
        }

        #endregion ui

        #region textures

        protected virtual Texture2D capturePointTextureFromTeam(int team)
        {
            switch (team)
            {
                case -1:
                    return ImageRegistry.capturePointNeutral.get();

                case 0:
                    return ImageRegistry.capturePointFriendly.get();

                case 1:
                    return ImageRegistry.capturePointEnemy.get();
            }

            return ImageRegistry.capturePointNeutral.get();
        }

        protected virtual Texture2D spawnPointTextureFromTeam(int team)
        {
            switch (team)
            {
                case -1:
                    return ImageRegistry.spawnPointNeutral.get();

                case 0:
                    return ImageRegistry.spawnPointFriendly.get();

                case 1:
                    return ImageRegistry.spawnPointEnemy.get();
            }

            return ImageRegistry.spawnPointNeutral.get();
        }

        #endregion textures

        protected override void mouseEnter(PointerEventData eventData)
        {
            setHovered(true);
        }

        protected override void mouseExit(PointerEventData eventData)
        {
            setHovered(false);
        }

        protected virtual void setHovered(bool isHovered)
        {
            if (spawnPoint.owner == ActorManager.instance.player.team)
                hoveredIcon.gameObject.SetActive(isHovered);
        }
    }
}