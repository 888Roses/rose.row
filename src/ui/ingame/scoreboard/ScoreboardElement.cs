using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;

namespace rose.row.ui.ingame.scoreboard
{
    /// <summary>
    /// Responsible for building and displaying scoreboard UI.
    /// </summary>
    public class ScoreboardElement : UiElement
    {
        #region constants

        /// <summary>
        /// The gap between the two team's window.
        /// </summary>
        public const float k_TeamWindowGap = 5f;

        #endregion

        #region building

        private ScoreboardTeamWindowElement _blue;
        private ScoreboardTeamWindowElement _red;

        public override void build()
        {
            _blue = createTeamWindow(0);
            _red = createTeamWindow(1);
        }

        private void Start()
        {
            foreach (var actor in ActorManager.instance.actors)
            {
                if (actor.team == 0)
                {
                    _blue.addEntry(actor);
                }

                if (actor.team == 1)
                {
                    _red.addEntry(actor);
                }
            }
        }

        private void Update()
        {
            //_blue.setAnchoredPosition(0, -_blue.getHeight() / 2 - k_TeamWindowGap / 2);
            //_red.setAnchoredPosition(0, _red.getHeight() / 2 + k_TeamWindowGap / 2);
            _blue.setAnchoredPosition(-ScoreboardTeamWindowElement.k_TeamWindowWidth / 2 - k_TeamWindowGap / 2, 0);
            _red.setAnchoredPosition(ScoreboardTeamWindowElement.k_TeamWindowWidth / 2 + k_TeamWindowGap / 2, 0);
        }

        private ScoreboardTeamWindowElement createTeamWindow(int team)
        {
            var window = UiFactory.createUiElement<ScoreboardTeamWindowElement>(
                name: "Team Window",
                this
            );
            window.build(team);

            return window;
        }

        #endregion building
    }
}