using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.ui.ingame.scoreboard
{
    public class ScoreboardTeamWindowHeaderElement : UiElement
    {
        /// <summary>
        /// The height of the team windows' header.
        /// Helps with determining the total height of a team's window.
        /// </summary>
        public const float k_ScoreboardHeaderHeight = 52f;

        public int team;

        public UiElement topWrapper;
        public UiElement factionIcon;
        public TextElement playerCountText;

        public UiElement bottomWrapper;
        public TextElement hashtagText;
        public TextElement playerNameText;
        public TextElement squadText;
        public TextElement scoreText;
        public TextElement capturesText;

        private float _bottomWrapperOffsetX;

        protected override void Awake() { }

        public void build(int team)
        {
            this.team = team;
            build();
        }

        public override void build()
        {
            setupElement();
            createTopWrapper();
            createBottomWrapper();
        }

        private void setupElement()
        {
            setAnchors(Anchors.StretchTop);
            setOffset(0, 0, 0, 0);
            setHeight(k_ScoreboardHeaderHeight);

            image().texture = team == 0
                ? ImageRegistry.scoreboardHeaderBlue.get()
                : ImageRegistry.scoreboardHeaderRed.get();
        }

        private void createTopWrapper()
        {
            void createWrapper()
            {
                topWrapper = UiFactory.createGenericUiElement("Top Wrapper", this);
                topWrapper.setAnchors(anchors: Anchors.StretchTop, updatePivot: true);
                topWrapper.setPivot(0.5f, 1);
                topWrapper.setAnchoredPosition(0, 0);
                topWrapper.setHeight(34f);
            }

            void createFactionIcon()
            {
                factionIcon = UiFactory.createGenericUiElement("Faction Icon", topWrapper);
                factionIcon.setAnchors(Anchors.StretchLeft);
                factionIcon.setPivot(0, 0.5f);
                factionIcon.setAnchoredPosition(0, 0);
                var fitter = factionIcon.use<AspectRatioFitter>();
                fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                fitter.aspectRatio = 1;
                factionIcon.image().texture = ImageRegistry.germanyFactionImageHighRes.get();
            }

            void createPlayerCountText()
            {
                playerCountText = createText("Players Text", topWrapper, "PLAYERS: 18");
                playerCountText.setTextAlign(HorizontalAlignmentOptions.Left);
                playerCountText.setFontSize(26);
                playerCountText.setAnchors(Anchors.StretchLeft);
                playerCountText.setPivot(0, 0.5f);
                playerCountText.setAnchoredPosition(factionIcon.getWidth() + 10f, 0);
                playerCountText.setWidth(400);
            }

            createWrapper();
            createFactionIcon();
            createPlayerCountText();
        }

        private void createBottomWrapper()
        {
            void createWrapper()
            {
                bottomWrapper = UiFactory.createGenericUiElement("Bottom Wrapper", this);
                bottomWrapper.setAnchors(anchors: Anchors.StretchBottom, updatePivot: true);
                bottomWrapper.setPivot(0.5f, 0);
                bottomWrapper.setAnchoredPosition(0, 0);
                bottomWrapper.setHeight(18f);
            }

            void fillWrapper()
            {
                _bottomWrapperOffsetX = 0;

                hashtagText = createStackedBottomText("Hashtag Text", "#", 16f - 2, 2);
                playerNameText = createStackedBottomText("Player Name Text", "PLAYER NAME", 183f);
                squadText = createStackedBottomText("Squad Text", "SQUAD", 119f);
                // TODO: Find a way to put "SCORE" instead of "XP" without it being too big in comparison to "CAPTURES".
                scoreText = createStackedBottomText("Score Text", "XP", 39f);
                capturesText = createStackedBottomText("Captures Text", "CAPTURES", 59f);

                createIcon("Kills", ImageRegistry.scoreboardKills.get(), 29f);
                createIcon("Deaths", ImageRegistry.scoreboardDeaths.get(), 29f);
                createIcon("Headshots", ImageRegistry.scoreboardHeadshots.get(), 29f);
                createIcon("Destroyed Tanks", ImageRegistry.scoreboardDestroyedTanks.get(), 29f);
                createIcon("Destroyed Planes", ImageRegistry.scoreboardDestroyedPlanes.get(), 29f);

                capturesText = createStackedBottomText("Ping Text", "PING", 30f);
            }

            createWrapper();
            fillWrapper();
        }

        private UiElement createIcon(string name, Texture2D texture, float totalWidth, float offsetX = 0f)
        {
            var iconContainer = UiFactory.createGenericUiElement($"{name} Container", bottomWrapper);
            iconContainer.setAnchors(Anchors.StretchLeft);
            iconContainer.setPivot(0, 0.5f);
            iconContainer.setAnchoredPosition(_bottomWrapperOffsetX + offsetX, 0);
            iconContainer.setWidth(totalWidth);

            var icon = UiFactory.createGenericUiElement(name, iconContainer);
            icon.image().texture = texture;
            icon.setSize(texture.width, texture.height);

            _bottomWrapperOffsetX = totalWidth + iconContainer.anchoredPosition.x;

            return iconContainer;
        }

        private TextElement createStackedBottomText(string name, string content, float width, float offsetX = 0f, float wrapperOffsetX = 1)
        {
            var text = createText(name, bottomWrapper, content);
            text.setAnchors(Anchors.StretchLeft);
            text.setPivot(0, 0.5f);
            text.setAnchoredPosition(_bottomWrapperOffsetX + offsetX, 0);
            text.setWidth(width);
            _bottomWrapperOffsetX = text.getWidth() + text.anchoredPosition.x + wrapperOffsetX;

            return text;
        }

        private TextElement createText(string name, UiElement parent, string content)
        {
            var text = UiFactory.createUiElement<TextElement>(name, parent);
            text.build();
            text.setColor("#C9C5BD");
            text.setFontSize(18);
            text.setFont(Fonts.defaultFont);
            text.setTextAlign(VerticalAlignmentOptions.Geometry);
            text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            text.setText(content);

            return text;
        }
    }
}