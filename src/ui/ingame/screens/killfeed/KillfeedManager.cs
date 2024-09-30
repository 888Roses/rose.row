using rose.row.data;
using rose.row.default_package;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.killfeed;
using rose.row.ui.ingame.scoreboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.ui.ingame.screens.killfeed
{
    public class KillfeedManager : Singleton<KillfeedManager>
    {
        // Pulled from Heroes & Generals files directly.
        public const float k_Width = 800f;

        public const float k_Height = 500f;

        public static ConstantHolder<int> maxItems = new ConstantHolder<int>(
            name: "ui.killfeed.max_visible_items",
            description: "The maximum amount of items that can be displayed at once. If an item is added after this number, the oldest item will be removed.",
            defaultValue: 10
        );

        public const float k_ItemsGap = 4f;

        public UiScreen uiScreen;

        public UiElement wrapper;
        public List<KillfeedItemElement> items;

        private void Awake()
        {
            items = new List<KillfeedItemElement>();
            uiScreen = UiFactory.createUiScreen("Canvas", ScreenOrder.killfeedMenu, transform);

            wrapper = UiFactory.createGenericUiElement("Wrapper", uiScreen.transform);
            wrapper.setAnchors(UiElement.Anchors.BottomCenter, false, false);
            wrapper.setPivot(0.5f, 0);
            //wrapper.setAnchors(new UiElement.LiteralAnchors(k_Width, 0.1f, 1f - k_Width, k_Height));
            wrapper.setHeight(k_Height);
            wrapper.setWidth(k_Width);
            wrapper.setAnchoredPosition(0, Screen.height * 0.1f);
        }

        public void addItem(AbstractKillfeedInfo info, Actor actor)
        {
            StartCoroutine(addItemCoroutine(info, actor));
        }

        private IEnumerator addItemCoroutine(AbstractKillfeedInfo info, Actor actor)
        {
            yield return new WaitForSeconds(0.1f);
            actuallyAddItem(info, actor);
        }

        private void actuallyAddItem(AbstractKillfeedInfo info, Actor actor)
        {
            Scoreboard.players[actor].score += info.getXP();

            if (actor.aiControlled)
                return;

            if (items.Count > 0)
            {
                var lastItem = items[items.Count - 1];
                if (lastItem.info.compare(info))
                {
                    lastItem.xp += info.getXP();
                    lastItem.update();
                    return;
                }
            }

            var item = UiFactory.createUiElement<KillfeedItemElement>("Feed Item", wrapper.rectTransform);
            item.onDispose += () => items.Remove(item);
            item.rebuild(info);
            items.Add(item);

            recalculateItemLayout();
        }

        private void recalculateItemLayout()
        {
            var stackHeight = 0f;

            while (items.Count > maxItems.get())
                items[0].dispose();

            for (int i = 0; i < items.Count; i++)
            {
                // Build from the bottom up.
                KillfeedItemElement item = items[items.Count - 1 - i];
                item.setAnchors(UiElement.Anchors.StretchBottom, updatePivot: true);
                item.setPivot(0.5f, 0);
                item.setHeight(KillfeedItemElement.k_Height);
                item.setAnchoredPosition(0, stackHeight);

                stackHeight += item.getHeight() + k_ItemsGap;
            }
        }
    }
}