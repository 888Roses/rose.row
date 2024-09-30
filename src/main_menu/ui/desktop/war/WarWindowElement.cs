using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.main_menu.ui.desktop.war.missions;

namespace rose.row.main_menu.ui.desktop.war
{
    public class WarWindowElement : UiElement
    {
        public const float k_BackgroundPadding = 24f;
        public const float k_WarMapWidth = 0.75f;

        private UiElement _container;
        private UiElement _background;
        private WarMapContainerElement _warMap;
        private MissionBarElement _missionBar;

        protected override void Awake()
        { }

        public override void build()
        {
            createContainer();
            createBackground();

            createWarMap();
            createMissionBar();
        }

        private void createMissionBar()
        {
            _missionBar = UiFactory.createUiElement<MissionBarElement>("Mission Bar", _container);
            _missionBar.setAnchors(new LiteralAnchors(k_WarMapWidth, 0, 1f, 1f));
            _missionBar.build();
        }

        // Omg gonna be so hard yaezaebghzehzebfyerbfehrf
        // I WAS RIGHT
        private void createWarMap()
        {
            _warMap = UiFactory.createUiElement<WarMapContainerElement>("War Map Container", _container);
            _warMap.setAnchors(new LiteralAnchors(0, 0, 1, 1));
            _warMap.build();
        }

        private void createContainer()
        {
            _container = UiFactory.createGenericUiElement("Container", this);
            _container.setAnchors(Anchors.FillParent);
        }

        private void createBackground()
        {
            _background = UiFactory.createGenericUiElement("Background", _container);
            _background.setAnchors(Anchors.FillParent);
            _background.image().texture = ImageRegistry.warWindowBackground.get();
        }
    }
}