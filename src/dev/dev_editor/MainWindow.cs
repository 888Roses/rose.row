using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.cursor;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.dev.dev_editor
{
    public class MainWindow : FloatingWindow
    {
        public override string name => "Main";
        public override bool isGameOnly => false;

        public override float containerPadding => 16f;
        public override float headerHeight => 32f;

        public const float k_ItemGap = 8f;

        #region building

        private VerticalListElement _list;

        private Button _openSandboxButton;
        private Button _freezeGameplay;
        private Button _lockCursor;
        private Button _forceAim;

        public override UiElement itemContainer => _list;

        public override void build()
        {
            hookEvents();

            base.build();

            _list = UiFactory.createUiElement<VerticalListElement>("List", container);
            _list.build();
            _list.setAnchors(Anchors.FillParent);
            _list.setChildControlWidth(true);
            _list.setChildForceExpandWidth(true);
            _list.setSpacing(k_ItemGap);

            _openSandboxButton = button("Open Sandbox", true, startTestingMap);

            header("In Game");
            var subtitle = text("Don't use those options when not in a lobby.");
            subtitle.setColor("#828282");
            subtitle.setFontSize(14f);

            _freezeGameplay = button("Freeze Gameplay", false, toggleGameplayFrozenState);
            _lockCursor = button("Lock Cursor", false, toggleLockedCursor);
            _forceAim = button("Enable Force Aim", false, forceAim);

            LayoutRebuilder.MarkLayoutForRebuild(_list.rectTransform);

            setSize(400, 236);
            updateHeight(_list.rectTransform, k_ItemGap);

            setAnchoredPosition(-Screen.width / 2 + getWidth() / 2 + 24f, Screen.height / 2 - getHeight() / 2 - 24);
        }

        #endregion

        private void hookEvents()
        {
            MouseCursor.cursorHandlers.Add(isCursorLocked);
        }

        private bool isCursorLocked(FpsActorController controller) => DevMainInfo.isCursorLocked;

        private void startTestingMap()
        {
            Debug.Log($"Trying to open a sandbox session...");
            DevEvents.onStartedSandboxLevel.before?.Invoke(true);

            try
            {
                if (GameManager.IsIngame())
                {
                    Debug.Log($"Trying to open a sandbox session whilst in-game. Returning to main menu...");
                    GameManager.ReturnToMenu();
                }

                MainMenu.instance.OpenPageIndex(MainMenu.PAGE_INSTANT_ACTION);
                var map = InstantActionMaps.instance
                                           .officialEntries
                                           .FirstOrDefault(x => x.metaData.displayName.ToLowerInvariant() == "island");
                GameManager.StartLevel(map, GameManager.instance.gameModeParameters);

                DevEvents.onStartedSandboxLevel.after?.Invoke(true);
                DevMainInfo.isInSandboxLevel = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to open a sandbox session. Reason:");
                Debug.LogException(e);
                DevEvents.onStartedSandboxLevel.after?.Invoke(false);
            }
        }

        private void forceAim()
        {
            DevMainInfo.forceAim = !DevMainInfo.forceAim;

            _forceAim.setText(DevMainInfo.forceAim ? "Disable Force Aim" : "Enable Force Aim");
            Debug.Log(DevMainInfo.forceAim ? "Enabled force aim." : "Disabled force aim.");
        }

        private void toggleLockedCursor()
        {
            DevMainInfo.isCursorLocked = !DevMainInfo.isCursorLocked;

            _lockCursor.setText(DevMainInfo.isCursorLocked ? "Unlock Cursor" : "Lock Cursor");
            Debug.Log(DevMainInfo.isCursorLocked ? "Locked cursor." : "Unlocked cursor.");
        }

        private void toggleGameplayFrozenState()
        {
            if (DevMainInfo.isGameplayFrozen)
            {
                unfreezeGameplay();
                _freezeGameplay.setText("Freeze Gameplay");
            }
            else
            {
                freezeGameplay();
                _freezeGameplay.setText("Unfreeze Gameplay");
            }
        }

        private void unfreezeGameplay()
        {
            Debug.Log("Unfreezed gameplay.");
            DevMainInfo.isGameplayFrozen = false;

            foreach (var actor in ActorManager.instance.actors)
            {
                if (actor.aiControlled)
                {
                    actor.Unfreeze();
                }
                else
                {
                    var controller = FpsActorController.instance;
                    controller.EnableInput();
                    controller.EnableMovement();
                }
            }
        }

        private void freezeGameplay()
        {
            Debug.Log("Freezed gameplay.");
            DevMainInfo.isGameplayFrozen = true;

            foreach (var actor in ActorManager.instance.actors)
            {
                if (actor.aiControlled)
                {
                    if (actor.IsSeated())
                        actor.LeaveSeat(false);

                    actor.Freeze();
                }
                else
                {
                    var controller = FpsActorController.instance;
                    controller.DisableInput();
                    controller.DisableMovement();
                }
            }
        }
    }
}
