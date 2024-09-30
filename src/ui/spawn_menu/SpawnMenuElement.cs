using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;

namespace rose.row.ui.spawn_menu
{
    public class SpawnMenuElement : UiElement
    {
        public DeployLocationTextElement deployText;
        public DeployingWindow deployingWindow;

        public override void build()
        {
            setAnchors(Anchors.FillParent);

            deployText = UiFactory.createUiElement<DeployLocationTextElement>(
                name: "Deploy Text",
                element: this);
            deployText.setAnchors(
                anchors: Anchors.StretchTop,
                updateOffsets: true,
                updatePivot: true);
            deployText.setAnchoredPosition(0, -40f);

            deployingWindow = UiFactory.createUiElement<DeployingWindow>(
                name: "Deploying Window",
                element: this);
            deployingWindow.setAnchors(Anchors.BottomCenter);
            deployingWindow.setPivot(0.5f, 0);
            deployingWindow.setSize(240, 70);
            deployingWindow.setAnchoredPosition(0, 8);
        }
    }
}
