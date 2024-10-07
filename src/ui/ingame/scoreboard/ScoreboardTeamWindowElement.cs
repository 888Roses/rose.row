using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using System.Collections.Generic;
using System.Linq;

namespace rose.row.ui.ingame.scoreboard
{
    public class ScoreboardTeamWindowElement : UiElement
    {
        #region constants

        /// <summary>
        /// Width of a team's score window.
        /// </summary>
        public const float k_TeamWindowWidth = 600f;

        #endregion constants

        #region events

        public void onScoreboardUpdate()
        {
            scoreboardEntries = scoreboardEntries.OrderByDescending(x => x.playerInfo.score).ToList();
            updateElements();
        }

        #endregion

        public int team;

        public ScoreboardTeamWindowHeaderElement header;
        public UiElement entryContainer;

        public List<ScoreboardEntryElement> scoreboardEntries;

        protected override void Awake() { }

        public void build(int team)
        {
            this.team = team;

            build();
        }

        public override void build()
        {
            scoreboardEntries = new List<ScoreboardEntryElement>();

            setSize(k_TeamWindowWidth, 200);
            header = UiFactory.createUiElement<ScoreboardTeamWindowHeaderElement>(
                name: "Header",
                element: this
            );
            header.build(team);

            entryContainer = UiFactory.createGenericUiElement("Entry Container", this);
            entryContainer.setAnchors(
                anchors: Anchors.FillParent,
                updateOffsets: true,
                updatePivot: false
            );
            entryContainer.setOffset(0, 0, 0, -header.getHeight() / 2);
        }

        private void Update()
        {
            setHeight(header.getHeight() + scoreboardEntries.Count * ScoreboardEntryElement.k_PlayerEntryHeight);
        }

        public void addEntry(Actor actor)
        {
            var scoreboardEntry = UiFactory.createUiElement<ScoreboardEntryElement>(
                name: $"Entry {actor.getNameSafe()}",
                element: entryContainer
            );

            scoreboardEntry.build(actor, scoreboardEntries.Count + 1);

            var offsetY = scoreboardEntries.Count * ScoreboardEntryElement.k_PlayerEntryHeight;
            scoreboardEntry.setAnchors(Anchors.StretchTop);
            scoreboardEntry.setHeight(ScoreboardEntryElement.k_PlayerEntryHeight);
            scoreboardEntry.setPivot(0, 1);
            scoreboardEntry.setAnchoredPosition(0f, -offsetY);

            scoreboardEntries.Add(scoreboardEntry);
            header.playerCountText.setText($"PLAYERS: {scoreboardEntries.Count}");
        }

        private void updateElements()
        {
            for (var i = 0; i < scoreboardEntries.Count; i++)
            {
                var offsetY = i * ScoreboardEntryElement.k_PlayerEntryHeight;
                scoreboardEntries[i].setAnchoredPosition(0f, -offsetY);
                scoreboardEntries[i].update();
            }
        }
    }
}