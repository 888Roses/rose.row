using rose.row.easy_package.ui.factory;
using rose.row.ui.cursor;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.ui.elements
{
    public interface IEnableable
    {
        bool isEnabled { get; }

        void enable();

        void disable();

        void setEnabled(bool enabled);
    }

    public abstract class FlexibleMenu : MonoBehaviour, IEnableable
    {
        public UiScreen uiScreen;

        #region IEnableable

        protected List<GameObject> _enabledGameObjects { get; } = new List<GameObject>();
        private bool _isEnabled = false;
        public bool isEnabled => _isEnabled;

        public void enable() => setEnabled(true);

        public void disable() => setEnabled(false);

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            foreach (var gameObject in _enabledGameObjects)
                gameObject.SetActive(_isEnabled);

            if (pauseGame)
            {
                if (enabled)
                    GameManager.PauseGame();
                else
                    GameManager.UnpauseGame();
            }
        }

        #endregion IEnableable

        #region Abstract Layer

        /// <summary>
        /// Whether or not the game should be paused upon this menu opening.
        /// </summary>
        public abstract bool pauseGame { get; }

        /// <summary>
        /// Whether or not to unlock the cursor whenever this menu is opeend.
        /// </summary>
        public virtual bool unlockCursor { get; } = true;

        public abstract void buildUi();

        #endregion Abstract Layer

        protected virtual void Awake()
        {
            MouseCursor.cursorHandlers.Add(hookCursor);

            createBaseUi();
            buildUi();
        }

        private void OnDestroy()
        {
            MouseCursor.cursorHandlers.Remove(hookCursor);
        }

        private bool hookCursor(FpsActorController controller) => _isEnabled;



        protected virtual void registerAsCursorUnlocker()
        {
            if (unlockCursor)
            {
                MouseCursor.cursorHandlers.Add((menu) => isEnabled);
            }
        }

        public virtual void createBaseUi()
        {
            uiScreen = UiFactory.createUiScreen("Screen", ScreenOrder.inGameMenu, transform);
        }
    }
}