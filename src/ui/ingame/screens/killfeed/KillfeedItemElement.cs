using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.killfeed;
using System;
using TMPro;
using UnityEngine;

namespace rose.row.ui.ingame.screens.killfeed
{
    public class KillfeedItemElement : UiElement
    {
        public const float k_Height = 24;

        // Pulled from Heroes & Generals files directly.
        // Fading starts immediately but takes 8 seconds to fade out completely.
        public const float k_Lifetime = 0f;

        public const float k_FadingTime = 8f;

        public AbstractKillfeedInfo info;
        public TextElement text;
        public CanvasGroup group;

        private float _startFadingTime;
        private bool _isFading;

        public Action onDispose;
        public int xp;

        public override void build()
        {
            text = UiFactory.createUiElement<TextElement>("Text", rectTransform);
            group = gameObject.AddComponent<CanvasGroup>();

            if (k_Lifetime == 0)
                startFading();
            else
                Invoke(nameof(startFading), k_Lifetime);
        }

        public void rebuild(AbstractKillfeedInfo info)
        {
            this.info = info;
            xp = info.getXP();

            text.setAnchors(Anchors.FillParent);
            text.setAllowRichText(true);
            text.setFontSize(24);
            text.setFont(Fonts.defaultFont);
            text.setColor(Color.white);
            text.setText(info.getMessage(xp));
            text.setTextAlign(HorizontalAlignmentOptions.Center);
            text.setTextAlign(VerticalAlignmentOptions.Middle);
            text.setShadow(new Vector2(1, -1), Color.black);
        }

        public void update()
        {
            text.setText(info.getMessage(xp));
        }

        public void startFading()
        {
            _startFadingTime = Time.time;
            _isFading = true;
        }

        private float timeElapsedSinceStartFading => Time.time - _startFadingTime;
        private float fadingInterpolationTime => timeElapsedSinceStartFading / k_FadingTime;

        private void Update()
        {
            if (_isFading)
            {
                group.alpha = Mathf.Lerp(1, 0, fadingInterpolationTime);

                if (group.alpha <= 0)
                {
                    dispose();
                    return;
                }
            }
        }

        public void dispose()
        {
            onDispose?.Invoke();
            Destroy(gameObject);
        }
    }
}