using rose.row.data;
using rose.row.default_package;
using rose.row.easy_events;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using UnityEngine;

namespace rose.row.ui.ingame.crosshair
{
    public class CrosshairBranchElement : UiElement
    { }

    public class CrosshairManager : Singleton<CrosshairManager>
    {
        public static void create()
        {
            var gameObject = new GameObject("Crosshair Manager");
            gameObject.AddComponent<CrosshairManager>();
        }

        public static readonly ConstantHolder<float> crosshairBranchLength = new ConstantHolder<float>(
            name: "ui.crosshair.branch_length",
            description: "The length of a crosshair branch. If the branch is vertical, this is the height; otherwise, this is the width.",
            defaultValue: 14f
        );

        public static readonly ConstantHolder<float> crosshairBranchThickness = new ConstantHolder<float>(
            name: "ui.crosshair.branch_thickness",
            description: "The thickness of a crosshair branch. If the branch is vertical, this is the width; otherwise, this is the height.",
            defaultValue: 3f
        );

        public static readonly ConstantHolder<float> hitmarkerLifetime = new ConstantHolder<float>(
            name: "ui.crosshair.hitmarker_lifetime",
            description: "How long will the hitmarker stay on screen when hitting someone?",
            defaultValue: 0.05f
        );

        public static readonly ConstantHolder<bool> hitmarkerVisibility = new ConstantHolder<bool>(
            name: "ui.crosshair.hitmarker",
            description: "Enables or disables hitmarkers.",
            defaultValue: false
        );

        public const int k_CrosshairBranchTop = 0;
        public const int k_CrosshairBranchBottom = 1;
        public const int k_CrosshairBranchRight = 2;
        public const int k_CrosshairBranchLeft = 3;

        public UiScreen _screen;

        public CrosshairBranchElement[] branches;
        public UiElement branchContainer;
        public UiElement hitmarker;

        public CrosshairBranchElement top => branches[k_CrosshairBranchTop];
        public CrosshairBranchElement bottom => branches[k_CrosshairBranchBottom];
        public CrosshairBranchElement right => branches[k_CrosshairBranchRight];
        public CrosshairBranchElement left => branches[k_CrosshairBranchLeft];

        public static void subscribeToInitializationEvents()
        {
            Events.onActorDie.after += onKillStatic;
            Events.onActorHurt.after += onDamageStatic;
        }

        private static void onKillStatic(Actor actor, DamageInfo info, bool isSilentKill)
        {
            if (instance != null)
            {
                instance.onKill(actor, info, isSilentKill);
            }
        }

        private static void onDamageStatic(Actor actor, DamageInfo info)
        {
            if (instance != null)
            {
                instance.onDamage(actor, info);
            }
        }

        private void Awake()
        {
            build();
        }

        private void onKill(Actor actor, DamageInfo info, bool isSilentKill)
        {
            if (!hitmarkerVisibility.get())
                return;

            if (info.isPlayerSource && info.sourceActor.team != actor.team && !actor.dead)
            {
                hitmarker.gameObject.SetActive(true);
                hitmarker.image().color = Color.red;
                Invoke(nameof(hideCrosshair), hitmarkerLifetime.get());
            }
        }

        private void onDamage(Actor actor, DamageInfo info)
        {
            if (!hitmarkerVisibility.get())
                return;

            if (info.isPlayerSource && info.sourceActor.team != actor.team && !actor.dead)
            {
                hitmarker.gameObject.SetActive(true);
                hitmarker.image().color = Color.white;
                Invoke(nameof(hideCrosshair), hitmarkerLifetime.get());
            }
        }

        private void hideCrosshair()
        {
            hitmarker.gameObject.SetActive(false);
        }

        private void Update()
        {
            updateCrosshairVisibility();
            updateCrosshairSpread();
        }

        private void build()
        {
            createScreen();
            createBranchContainer();
            createBranches();
            createHitmarker();
        }

        private void createScreen()
        {
            _screen = UiFactory.createUiScreen(
                name: "Crosshair",
                order: ScreenOrder.crosshair,
                parent: transform
            );
        }

        private void createHitmarker()
        {
            hitmarker = UiFactory.createGenericUiElement("Hitmarker", _screen);
            hitmarker.setSize(40f);
            hitmarker.image().texture = ImageRegistry.hitmarker.get();
            hitmarker.gameObject.SetActive(false);
        }

        private void createBranchContainer()
        {
            branchContainer = UiFactory.createGenericUiElement(
                name: "Container",
                screen: _screen
            );

            var group = branchContainer.use<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        private void createBranches()
        {
            branches = new CrosshairBranchElement[4];

            branches[k_CrosshairBranchTop] = createBranch(
                name: "Top",
                element: branchContainer,
                isHorizontal: false,
                isPositive: true
            );

            branches[k_CrosshairBranchBottom] = createBranch(
                name: "Bottom",
                element: branchContainer,
                isHorizontal: false,
                isPositive: false
            );

            branches[k_CrosshairBranchRight] = createBranch(
                name: "Right",
                element: branchContainer,
                isHorizontal: true,
                isPositive: true
            );

            branches[k_CrosshairBranchLeft] = createBranch(
                name: "Left",
                element: branchContainer,
                isHorizontal: true,
                isPositive: false
            );
        }

        private CrosshairBranchElement createBranch(string name, UiElement element, bool isHorizontal, bool isPositive)
        {
            var branch = UiFactory.createUiElement<CrosshairBranchElement>(name, element);

            #region anchors

            UiElement.Anchors anchors;
            if (isHorizontal)
            {
                if (isPositive)
                {
                    anchors = UiElement.Anchors.MiddleRight;
                }
                else
                {
                    anchors = UiElement.Anchors.MiddleLeft;
                }
            }
            else
            {
                if (isPositive)
                {
                    anchors = UiElement.Anchors.TopCenter;
                }
                else
                {
                    anchors = UiElement.Anchors.BottomCenter;
                }
            }

            branch.setAnchors(anchors, false, false);

            if (isHorizontal)
            {
                if (isPositive)
                {
                    branch.setPivot(1, 0.5f);
                }
                else
                {
                    branch.setPivot(0, 0.5f);
                }
            }
            else
            {
                if (isPositive)
                {
                    branch.setPivot(0.5f, 1);
                }
                else
                {
                    branch.setPivot(0.5f, 0);
                }
            }

            #endregion anchors

            #region size

            if (isHorizontal)
            {
                branch.setSize(crosshairBranchLength.get(), crosshairBranchThickness.get());
            }
            else
            {
                branch.setSize(crosshairBranchThickness.get(), crosshairBranchLength.get());
            }

            branch.setAnchoredPosition(0, 0);

            #endregion size

            #region image

            branch.image().texture = isHorizontal
                ? ImageRegistry.crosshairHorizontal.get()
                : ImageRegistry.crosshairVertical.get();

            #endregion image

            return branch;
        }

        private void updateCrosshairSpread()
        {
            var spread = 0f;
            var player = FpsActorController.instance;
            if (player != null && player.actor.activeWeapon != null)
                spread = player.actor.activeWeapon.GetCurrentSpreadMagnitude();
            var screenSpread = Screen.height * spread;
            //GUI.Label(new Rect(10, 200, 200, 100), $"Spread: {spread}\nScreen Spread: {screenSpread}");

            if (PlayerFpParent.instance != null)
            {
                branchContainer.setSize(Vector2.one * (screenSpread + crosshairBranchLength.get() * 2));
            }
        }

        private void updateCrosshairVisibility()
        {
            var group = branchContainer.use<CanvasGroup>();
            var alpha = 1f;

            var player = FpsActorController.instance;
            if (player != null && player.actor.activeWeapon != null)
            {
                alpha = 1f - player.actor.activeWeapon.getAimingAmount();
            }

            group.alpha = alpha;
        }
    }
}