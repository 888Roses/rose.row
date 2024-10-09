using HarmonyLib;
using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using TMPro;
using UnityEngine;

namespace rose.row.ui.spawn_menu
{
    public class DeployingWindow : UiElement
    {
        private UiElement _icon;
        private StandardTextElement _deployingText;
        private StandardTextElement _travellingText;
        private ProgressBarElement _progress;
        private CanvasGroup _canvasGroup;

        public override void build()
        {
            image().texture = ImageRegistry.spawnMenuDeployingBackground.get();

            _icon = UiFactory.createGenericUiElement(
                name: "Icon",
                element: this);
            var tex = ImageRegistry.spawnMenuHomeIcon.get();
            _icon.image().texture = tex;
            _icon.setAnchors(Anchors.TopRight, true, true);
            _icon.setAnchoredPosition(-10, -10);
            _icon.setSize(tex.width, tex.height);

            _progress = UiFactory.createUiElement<ProgressBarElement>(
                name: "Progress",
                element: this);
            _progress.setAnchors(Anchors.StretchBottom);
            _progress.setPivot(0.5f, 0);
            _progress.setOffset(18, 18, 0, 0);
            _progress.setAnchoredPosition(0, 10);
            _progress.setHeight(12f);

            _deployingText = UiFactory.createUiElement<StandardTextElement>(
                name: "Deploying Text",
                element: this);
            _deployingText.content = "DEPLOYING...";
            _deployingText.build();
            _deployingText.setAnchors(Anchors.TopLeft, updatePivot: true);
            _deployingText.setPivot(0, 1);
            _deployingText.text.setFontSize(27f);
            _deployingText.text.setTextAlign(
                HorizontalAlignmentOptions.Left,
                VerticalAlignmentOptions.Geometry);
            _deployingText.setSize(400, 0f);
            _deployingText.setAnchoredPosition(8, -24);

            _travellingText = UiFactory.createUiElement<StandardTextElement>(
                name: "Deploying Text",
                element: this);
            _travellingText.content = "Traveling to Location";
            _travellingText.build();
            _travellingText.setAnchors(Anchors.TopLeft, updatePivot: true);
            _travellingText.setPivot(0, 1);
            _travellingText.text.setFontSize(18f);
            _travellingText.text.setTextAlign(
                HorizontalAlignmentOptions.Left,
                VerticalAlignmentOptions.Geometry);
            _travellingText.setSize(400, 0f);
            _travellingText.setAnchoredPosition(8, -24 - 18f);

            _canvasGroup = use<CanvasGroup>();
        }

        private void Update()
        {
            var gameModeBase = Traverse
                                    .Create<GameModeBase>()
                                    .Field("activeGameMode");

            if (gameModeBase != null)
            {

                var respawnTime = GameManager.GameParameters().respawnTime;
                var remaining = (float) gameModeBase
                                            .Field("respawnWaveAction")
                                            .Method("Remaining")
                                            .GetValue();
                _progress.setProgress(1 - Mathf.Clamp01(remaining / respawnTime));
            }
            else
            {
                _progress.setProgress(0f);
            }

            _canvasGroup.setEnabled(FpsActorController.instance.hasAcceptedLoadoutAfterDeath
                && (bool) Traverse.Create(LoadoutUi.instance).Field("hasAcceptedLoadoutOnce").GetValue());
        }
    }
}
