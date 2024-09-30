using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.match;
using TMPro;
using UnityEngine;

namespace rose.row.main_menu.ui.desktop.war.missions
{
    public class MissionElement : UiElement
    {
        public const float k_Height = 110;
        public const float k_HeaderHeight = 32;

        public WarMission mission;

        private UiElement _header;
        private TextElement _title;
        private UiElement _background;
        private MissionEnterCombatButtonElement _enterCombat;

        protected override void Awake()
        { }

        public override void build()
        {
            createHeader();
            createBackground();
            createEnterCombatButton();
        }

        private void createEnterCombatButton()
        {
            _enterCombat = UiFactory.createUiElement<MissionEnterCombatButtonElement>("Enter Combat", _background);
            _enterCombat.setAnchors(Anchors.TopCenter, false, false);
            _enterCombat.setSize(132, 32);
            _enterCombat.setPivot(0.5f, 1);
            _enterCombat.setAnchoredPosition(0, -k_HeaderHeight - 4);
            _enterCombat.buttonText = "ENTER COMBAT";
            _enterCombat.build();

            _enterCombat.onClicked += executeMission;
        }

        private void executeMission()
        {
            MatchManager.startGame(mission);
        }

        private void createBackground()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);
            _background.setOffset(0, 0, 0, 0);
            _background.image().texture = ImageRegistry.fieldShadow.get();
            _background.moveToBack();
        }

        private void createHeader()
        {
            _header = UiFactory.createGenericUiElement("Header", this);
            _header.setAnchors(Anchors.StretchTop);
            _header.setPivot(0, 1);
            _header.setAnchoredPosition(0, 0);
            _header.setHeight(k_HeaderHeight);
            _header.image().color = new Color32(49, 49, 49, 255);

            createTitle();
        }

        private void createTitle()
        {
            _title = UiFactory.createUiElement<TextElement>("Title", _header);
            _title.setAnchors(Anchors.FillParent);
            _title.build();

            _title.setColor(Color.white);
            _title.setFont(Fonts.defaultFont);
            _title.setFontSize(20f);
            _title.setFontWeight(FontWeight.Bold);

            var action = (mission.type == WarMission.MissionType.Attack)
                ? "ATTACK"
                : "DEFEND";

            _title.setText($"{action} {mission.city.name.ToUpperInvariant()}");
            _title.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _title.setTextAlign(VerticalAlignmentOptions.Geometry);
        }
    }
}