using rose.row.easy_events;
using rose.row.easy_package.ui.factory;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace rose.row.ui.ingame.ingame_displayables
{
    /// <summary>
    /// An Ui manager that's responsible for displaying <see cref="AbstractDisplayable"/>s.
    /// </summary>
    public static class DisplayableUi
    {
        #region initialization events

        private static bool _hasInitializedEvents = false;

        /// <summary>
        /// Hooks base events once only, so that it doesn't have to do it everytime a
        /// game starts for instance.
        /// </summary>
        public static void subscribeToInitializationEvents()
        {
            if (!_hasInitializedEvents)
            {
                Events.onGameManagerStartLevel.after += onGameManagerStartLevelEvent;

                _hasInitializedEvents = true;
            }
        }

        /// <summary>
        /// Initialize every components of the DisplayableUi when the game starts.
        /// </summary>
        private static void onGameManagerStartLevelEvent()
        {
            initializeScreen();
        }

        #endregion initialization events

        #region displayables list

        private static List<AbstractDisplayable> _displayables;
        public static List<AbstractDisplayable> displayables => _displayables;

        /// <summary>
        /// Refreshes the <see cref="displayables"/> array, to match the current world
        /// <see cref="AbstractDisplayable"/>s.
        /// </summary>
        public static void refreshDisplayables()
        {
            _displayables = new(Object.FindObjectsOfType<AbstractDisplayable>());
        }

        #endregion displayables list

        #region displayables screen

        private static UiScreen _displayablesScreen;
        public static UiScreen displayablesScreen => _displayablesScreen;

        /// <summary>
        /// Initializes every component of the displayables screen.
        /// </summary>
        public static void initializeScreen()
        {
            Debug.Log($"Initializing Displayable UI.");

            _displayablesScreen = UiFactory.createUiScreen(
                name: "Displayables Screen",
                order: ScreenOrder.displayablesScreen,
                parent: null
            );

            // So that we have access to every displayable loaded in the scene.
            refreshDisplayables();
            refreshDisplayableWidgets();

            Debug.Log($"Finished initializing displayable UI.");
        }

        #endregion displayables screen

        #region displayable widgets

        private static List<DisplayableUiWidget> _createdWidgets;

        /// <summary>
        /// Resets the <see cref="_createdWidgets"/> list so that it doesn't contain any item.
        /// </summary>
        private static void resetCreatedWidgetsList()
        {
            if (_createdWidgets == null)
            {
                _createdWidgets = new List<DisplayableUiWidget>();
            }
            else
            {
                foreach (var widget in _createdWidgets)
                    if (widget != null)
                        Object.Destroy(widget.gameObject);

                _createdWidgets.Clear();
            }
        }

        /// <summary>
        /// Refreshes the displayable list to get all <see cref="AbstractDisplayable"/>s in the world and recreates every
        /// UI widgets on screen based on those displayables.
        /// </summary>
        /// <param name="forceRefreshWorldDisplayables">
        /// If passed True, forces this method to use <see cref="refreshDisplayables"/> to refresh all of the displayables in the world.
        /// </param>
        public static void refreshDisplayableWidgets(bool forceRefreshWorldDisplayables = false)
        {
            resetCreatedWidgetsList();

            if (_displayables == null || forceRefreshWorldDisplayables)
            {
                refreshDisplayables();
            }

            foreach (var displayable in _displayables)
            {
                if (displayable == null)
                    continue;

                _createdWidgets.Add(createDisplayableWidget(displayable));
            }
        }

        /// <summary>
        /// Creates a new displayable widget for the given <see cref="AbstractDisplayable"/>.
        /// </summary>
        /// <param name="displayable">The displayable whose widget will be created.</param>
        /// <returns>The newly created displayable ui widget.</returns>
        public static DisplayableUiWidget createDisplayableWidget(AbstractDisplayable displayable)
        {
            try
            {
                var widget = (DisplayableUiWidget) UiFactory.createUiElement(
                    type: displayable.displayableUiWidgetClass,
                    name: $"Widget {displayable.gameObject.name}",
                    screen: displayablesScreen
                );
                widget.build(displayable);
                return widget;
            }
            catch /*(Exception e)*/
            {
                // Debug.LogError($"Could not create displayable widget for displayable '{displayable.GetType().Name}':");
                // Debug.LogException(e);

                var widget = UiFactory.createUiElement<DisplayableUiWidget>(
                    name: $"Widget {displayable.gameObject.name}",
                    screen: displayablesScreen
                );
                widget.build(displayable);
                return widget;
            }
        }

        #endregion displayable widgets
    }
}