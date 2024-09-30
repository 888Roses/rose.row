using rose.row.easy_package.ui.factory.elements;

namespace rose.row.main_menu.ui.desktop.war.missions
{
    public abstract class AbstractButtonElement : UiElement
    {
        public abstract class AbstractButtonState
        {
            public AbstractButtonElement parent;

            public AbstractButtonState(AbstractButtonElement parent)
            {
                this.parent = parent;
            }

            public abstract void apply();
        }

        protected AbstractButtonState _currentState;
        public AbstractButtonState currentState => _currentState;

        public void setState(AbstractButtonState state)
        {
            _currentState = state;
            _currentState.apply();
        }
    }
}