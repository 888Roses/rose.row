using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables
{
    public class DisplayableUiWidget : UiElement
    {
        public const float k_PositionSmoothness = 40f;

        public AbstractDisplayable displayable;

        public Vector3 displayablePosition
            => displayable.transform.position + displayable.offset;

        private Vector3 _targetPosition;

        protected override void Awake()
        { }

        public void build(AbstractDisplayable displayable)
        {
            this.displayable = displayable;
            build();
        }

        public override void build()
        {
            image().texture = displayable.icon;
            setSize(displayable.iconSize);

            updatePositionOnScreen();

            var canvasGroup = use<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void LateUpdate()
        {
            updatePositionOnScreen();
        }

        public void updatePositionOnScreen()
        {
            var fpParent = PlayerFpParent.instance;
            if (fpParent == null)
                return;

            var playerCamera = Camera.main;
            calculateScreenPosition(playerCamera);
            calculateAlpha(playerCamera);
        }

        private void calculateScreenPosition(Camera playerCamera)
        {
            _targetPosition = playerCamera.WorldToScreenPoint(displayablePosition);
            transform.position = _targetPosition;
        }

        /// <summary>
        /// Fades the alpha of this widget based on distance from camera, angle and on
        /// distance from the edges of the screen.
        /// </summary>
        private void calculateAlpha(Camera playerCamera)
        {
            var viewportPos = playerCamera.WorldToViewportPoint(displayablePosition).with(z: 0);
            var distanceX = 1 - Mathf.Abs(0.5f - viewportPos.x);
            var distanceY = 1 - Mathf.Abs(0.5f - viewportPos.y);
            var baseAlpha = Mathf.Min(distanceX, distanceY);

            var dst = Vector3.Distance(displayablePosition, playerCamera.transform.position);
            var distanceMultiplier = Mathf.Floor(Mathf.Min(1, displayable.maxVisibleDistance / dst) * 10) / 10f;

            var dir = 1 - Vector3.Dot(
                lhs: (playerCamera.transform.position - displayablePosition),
                rhs: playerCamera.transform.forward
            );
            var directionMultiplier = Mathf.Max(0, dir);

            var canvasGroup = use<CanvasGroup>();
            canvasGroup.alpha = Mathf.Min(baseAlpha * distanceMultiplier, directionMultiplier);
        }
    }
}