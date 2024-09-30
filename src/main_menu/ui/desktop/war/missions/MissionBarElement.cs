using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.main_menu.ui.desktop.war.missions
{
    public class MissionBarElement : UiElement
    {
        public const float k_HeaderHeight = 40;

        private UiElement _headerContainer;
        private TextElement _headerTitle;
        private UiElement _missionContainer;
        private VerticalListElement _list;
        private ScrollableElement _scrollable;

        private List<MissionElement> _missions = new List<MissionElement>();

        protected override void Awake()
        {
        }

        public override void build()
        {
            image().color = new Color32(33, 33, 33, 225);

            createHeader();
            createMissionContainer();

            _scrollable = UiFactory.createUiElement<ScrollableElement>(
                name: "Scrollable",
                element: _missionContainer
            );
            _scrollable.build();
            _scrollable.setAnchors(Anchors.FillParent);

            _scrollable.container.image();
            _scrollable.container.use<Mask>().showMaskGraphic = false;

            _list = UiFactory.createUiElement<VerticalListElement>("List", _scrollable.container);
            _list.build();
            _list.setAnchors(Anchors.StretchTop);
            _list.setPivot(0.5f, 1f);
            _list.setHeight(400f);
            _list.setSpacing(4);
            _list.setPadding(8);
            _list.setChildForceExpandWidth(true);
            _list.setChildControlWidth(true);
            _list.use<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            _scrollable.setContent(_list);

            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());
            createMissionElement(WarMission.generateRandom());

            LayoutRebuilder.MarkLayoutForRebuild(_list.rectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(_scrollable.rectTransform);
        }

        private void createMissionContainer()
        {
            _missionContainer = UiFactory.createGenericUiElement("Mission Container", this);
            _missionContainer.setAnchors(Anchors.FillParent);
            _missionContainer.setOffset(0, 0, 0, -k_HeaderHeight);
        }

        public MissionElement createMissionElement(WarMission mission)
        {
            var element = UiFactory.createUiElement<MissionElement>("Mission", _list);
            element.mission = mission;
            element.build();
            element.setHeight(MissionElement.k_Height);
            _missions.Add(element);

            return element;
        }

        private void createHeader()
        {
            _headerContainer = UiFactory.createGenericUiElement("Header Container", this);
            _headerContainer.setPivot(0.5f, 1);
            _headerContainer.setAnchors(Anchors.StretchTop);
            _headerContainer.setHeight(k_HeaderHeight);
            _headerContainer.image().color = Color.black.with(a: 0.5f);

            _headerTitle = UiFactory.createUiElement<TextElement>("Title", _headerContainer);
            _headerTitle.build();
            _headerTitle.setAnchors(Anchors.FillParent);
            _headerTitle.setColor(Color.white);
            _headerTitle.setText("MISSIONS");
            _headerTitle.setFont(Fonts.defaultFont);
            _headerTitle.setFontSize(28f);
            _headerTitle.setTextAlign(HorizontalAlignmentOptions.Center);
            _headerTitle.setTextAlign(VerticalAlignmentOptions.Middle);
        }
    }
}