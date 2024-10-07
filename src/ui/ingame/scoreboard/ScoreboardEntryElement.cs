using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using TMPro;

namespace rose.row.ui.ingame.scoreboard
{
    public class ScoreboardEntryElement : UiElement
    {
        /// <summary>
        /// The height of one player entry in the scoreboard.
        /// Helps with determining the total height of a team's window.
        /// </summary>
        public const float k_PlayerEntryHeight = 16f;

        private float _stack;

        public Actor actor;
        public PlayerInfo playerInfo => Scoreboard.players[actor];
        public int scoreboardIndex;

        public TextElement scoreboardIndexText;

        public UiElement rankImage;
        public TextElement nameText;

        public TextElement squadText;
        public TextElement scoreText;
        public TextElement capturesText;
        public TextElement killsText;
        public TextElement deathsText;
        public TextElement headshotsText;
        public TextElement destroyedTanksText;
        public TextElement destroyedPlanesText;
        public UiElement ping;

        protected override void Awake() { }

        public void build(Actor actor, int scoreboardIndex)
        {
            this.actor = actor;
            this.scoreboardIndex = scoreboardIndex;

            build();
        }

        private string getScoreboardIndex()
        {
            if (scoreboardIndex < 10)
            {
                return $"0{scoreboardIndex}";
            }
            else
            {
                return scoreboardIndex.ToString();
            }
        }

        private string toScoreboardValue(int value)
        {
            return value == 0 ? "" : value.ToString();
        }

        public void update()
        {
            scoreText.setText(toScoreboardValue(playerInfo.score));
            capturesText.setText(toScoreboardValue(playerInfo.captures));
            killsText.setText(toScoreboardValue(playerInfo.kills));
            deathsText.setText(toScoreboardValue(playerInfo.deaths));
            headshotsText.setText(toScoreboardValue(playerInfo.headshots));
            destroyedTanksText.setText(toScoreboardValue(playerInfo.destroyedTanks));
            destroyedPlanesText.setText(toScoreboardValue(playerInfo.destroyedPlanes));
        }

        public override void build()
        {
            setHeight(k_PlayerEntryHeight);
            image().texture = actor.isPlayer() ? ImageRegistry.scoreboardPlayerEntrySelected.get() : ImageRegistry.scoreboardPlayerEntry.get();

            scoreboardIndexText = addToStack("Scoreboard Index Text", getScoreboardIndex(), 15f, autoColourText: false);
            createNameText();
            squadText = addToStack("Squad Text", playerInfo.squad, 119f);
            scoreText = addToStack("Score Text", playerInfo.score.ToString(), 39f);
            capturesText = addToStack("Captures Text", playerInfo.captures.ToString(), 59f);
            killsText = addToStack("Kills Text", playerInfo.kills.ToString(), 29f);
            deathsText = addToStack("Deaths Text", playerInfo.deaths.ToString(), 29f);
            headshotsText = addToStack("Headshots Text", playerInfo.headshots.ToString(), 29f);
            destroyedTanksText = addToStack("Destroyed Tanks Text", playerInfo.destroyedTanks.ToString(), 29f);
            destroyedPlanesText = addToStack("Destroyed Planes Text", playerInfo.destroyedPlanes.ToString(), 29f);
            ping = createPing();
        }

        private UiElement createPing()
        {
            var iconContainer = UiFactory.createGenericUiElement($"Ping Container", this);
            iconContainer.setAnchors(Anchors.StretchLeft);
            iconContainer.setPivot(0, 0.5f);
            iconContainer.setAnchoredPosition(_stack, 0);
            iconContainer.setWidth(29f);

            var icon = UiFactory.createGenericUiElement(name, iconContainer);
            var pingTextures = actor.isPlayer()
                ? ImageRegistry.scoreboardPingSquad[playerInfo.ping]
                : ImageRegistry.scoreboardPing[playerInfo.ping];
            var texture = pingTextures.get();
            icon.image().texture = texture;
            icon.setSize(texture.width, texture.height);

            return iconContainer;
        }

        private void createNameText()
        {
            nameText = addToStack("Name Text", playerInfo.name, 183f, false, true);
            var storedPos = nameText.anchoredPosition;
            nameText.setAnchoredPosition(storedPos.x + k_PlayerEntryHeight + 4f, storedPos.y);

            rankImage = UiFactory.createGenericUiElement("Rank", this);
            rankImage.setAnchors(Anchors.StretchLeft);
            rankImage.setPivot(0, 0.5f);
            rankImage.setAnchoredPosition(storedPos);
            rankImage.setWidth(k_PlayerEntryHeight);
            rankImage.image().texture = playerInfo.faction.factionRanks[playerInfo.rank].get();
        }

        private TextElement addToStack(string name, string content, float width, bool centerHorizontal = true, bool autoColourText = true)
        {
            var text = createRegularText(name, this, autoColourText);
            text.setText(content);
            text.setAnchors(Anchors.StretchLeft);
            text.setPivot(0, 0.5f);
            text.setAnchoredPosition(_stack, 0);
            text.setWidth(width);

            text.setTextAlign(VerticalAlignmentOptions.Geometry);

            if (centerHorizontal)
                text.setTextAlign(HorizontalAlignmentOptions.Geometry);

            _stack = text.anchoredPosition.x + width + 1;
            return text;
        }

        private TextElement createRegularText(string name, UiElement parent, bool autoColourText = true)
        {
            var text = UiFactory.createUiElement<TextElement>(name, parent);
            text.build();
            text.setFont(Fonts.defaultFont);
            text.setColor((actor.isPlayer() && autoColourText) ? "#B3E46E" : "#CBCBC9");

            text.setFontSize(18);

            return text;
        }
    }
}