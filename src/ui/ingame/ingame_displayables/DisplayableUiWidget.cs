using rose.row.easy_package.ui.factory.elements;
using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables
{
    public class DisplayableUiWidget : UiElement
    {
        public AbstractDisplayable displayable;

        public Vector3 displayablePosition
            => displayable != null
                ? displayable.transform.position + displayable.offset
                : Vector3.zero;

        public virtual bool isCanvasGroupInteractable => false;

        protected override void Awake()
        { }

        public virtual void build(AbstractDisplayable displayable)
        {
            this.displayable = displayable;
            build();
        }

        public override void build()
        {
            image().texture = displayable.icon;
            setSize(displayable.iconSize);

            updatePositionOnScreen();
            createCanvasGroup();
        }

        protected virtual void createCanvasGroup()
        {
            var canvasGroup = use<CanvasGroup>();
            canvasGroup.interactable = isCanvasGroupInteractable;
            canvasGroup.blocksRaycasts = isCanvasGroupInteractable;
        }

        protected virtual void LateUpdate()
        {
            updatePositionOnScreen();
        }

        public virtual void updatePositionOnScreen()
        {
            var fpParent = PlayerFpParent.instance;
            if (fpParent == null)
                return;

            var playerCamera = Camera.main;
            calculateScreenPosition(playerCamera);
            calculateAlpha(playerCamera);
        }

        protected virtual void calculateScreenPosition(Camera playerCamera)
        {
            transform.position = playerCamera.WorldToScreenPoint(displayablePosition);
        }

        protected virtual void calculateAlpha(Camera playerCamera)
        {
            var canvasGroup = use<CanvasGroup>();
            canvasGroup.alpha = displayable.getAlpha(playerCamera);
        }
    }
}