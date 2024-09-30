using rose.row.actor.health;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System.Text;
using TMPro;
using UnityEngine;

namespace rose.row.dev.dev_info_screen
{
    public class PlayerHealthDeveloperMenu : AbstractDeveloperMenu
    {
        public override Shortcut activateKey => new Shortcut(
            modifiers: new KeyCode[] { KeyCode.LeftControl },
            key: KeyCode.H);

        #region warning text

        public TextElement warningText;

        private void createWarningText()
        {
            warningText = createStandardText(
                name: "Warning Text",
                text: "You may only display health in-game!\n<size=50%><#FFFFFF50>PRESS <b>CTRL + H</b> TO CLOSE</color></size>",
                this);
            warningText.setAnchors(Anchors.FillParent);
            warningText.setTextAlign(HorizontalAlignmentOptions.Geometry);
            warningText.setTextAlign(VerticalAlignmentOptions.Geometry);
            warningText.setFontSize(32f);
            warningText.setAllowRichText(true);
            warningText.setInteractable(false);
        }

        #endregion

        #region debug texts

        private UiElement _debugWrapper;

        private TextElement _playerHealthStatsText;

        private void createDebugTexts()
        {
            _debugWrapper = UiFactory.createGenericUiElement("Wrapper", this);
            _debugWrapper.setAnchors(Anchors.FillParent);

            _playerHealthStatsText = createStandardText("Stats", "", _debugWrapper);
            _playerHealthStatsText.setAnchors(Anchors.FillParent);
            _playerHealthStatsText.setOffset(24, 24, -24, -24);
            _playerHealthStatsText.setInteractable(false);
            _playerHealthStatsText.setAllowRichText(true);
            _playerHealthStatsText.setShadow(1f, Color.black);
        }

        private void updateDebugTexts()
        {
            if (FpsActorController.instance == null || FpsActorController.instance.actor == null)
                return;

            var stringBuilder = new StringBuilder();
            var health = FpsActorController.instance.actor.GetComponent<CustomActorHealth>();
            stringBuilder.AppendLine("<#ffffff>[Health Info (Ctrl + H)]</color><#CBCEC1>");
            stringBuilder.AppendLine($"Health: {health.health}");
            stringBuilder.AppendLine($"Health Bars: {health.healthBars}");
            stringBuilder.AppendLine($"Health Floor: {health.healthFloor}");
            stringBuilder.AppendLine($"Maximum Health For Floor: {health.healthFloorMaxHealth}");
            stringBuilder.AppendLine($"Regeneration Time Left: {Mathf.Max(0, health.lastTimeHurt + CustomActorHealth.regenerationStartDelay.get() - Time.time)}");

            _playerHealthStatsText.setText(stringBuilder.ToString());
        }

        #endregion

        public override void build()
        {
            base.build();

            createWarningText();
            createDebugTexts();

            updateNotInGame();
        }

        private void Update()
        {
            if (GameManager.IsIngame())
            {
                updateInformation();
            }
            else
            {
                updateNotInGame();
            }
        }

        private void updateNotInGame()
        {
            warningText.gameObject.SetActive(true);
            _debugWrapper.gameObject.SetActive(false);
        }

        private void updateInformation()
        {
            warningText.gameObject.SetActive(false);
            _debugWrapper.gameObject.SetActive(true);
            updateDebugTexts();
        }
    }
}