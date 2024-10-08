using rose.row.data;
using rose.row.default_package;
using rose.row.easy_package.audio;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.ingame.scoreboard;
using rose.row.util;
using UnityEngine;

namespace rose.row.ui.ingame.match_start
{
    // TODO: Replace this simple way of doing it with a more convulated way which involves re-usability for messages like "United States Neutralized ......".
    public class MatchStartTimerElement : UiElement
    {
        public override void build()
        {
            setSize(ScoreboardTeamWindowElement.k_TeamWindowWidth, 32);
            setBackground(ImageRegistry.startMatchTimerBackground.get());
            setBackgroundColor(Color.white.with(a: 1f));
        }
    }

    public class MatchStartElement : UiElement
    {
        private UiElement _cover;
        private MatchStartTimerElement _timer;

        public override void build()
        {
            setAnchors(Anchors.FillParent);

            // So that the player cannot interract with the environment yet.
            _cover = UiFactory.createGenericUiElement("Ui Cover", this);
            _cover.setBackgroundColor(new Color32(0, 0, 0, 1));
            _cover.setAnchors(Anchors.FillParent);

            //_timer = UiFactory.createUiElement<MatchStartTimerElement>("Timer", _cover);
        }
    }

    public class MatchStartScreen : Singleton<MatchStartScreen>
    {
        public static readonly ConstantHolder<float> k_MatchStartScreenLifetime = new(
            name: "match_start_lifetime",
            description: "The time before a match starts.",
            defaultValue: 5f
        );

        public static void create()
        {
            var gameObject = new GameObject("Match Start Screen");
            gameObject.AddComponent<MatchStartScreen>();
        }

        private void Start()
        {
            _lifetime = k_MatchStartScreenLifetime.get();
            InvokeRepeating("playSoundEachSecond", 0, 1);
        }

        private int _soundIndex = 0;
        private void playSoundEachSecond()
        {
            Audio.play(AudioRegistry.startMatchTickingSounds[_soundIndex].get(), mixer: AudioMixer.Important);

            _soundIndex++;
            if (_soundIndex > AudioRegistry.startMatchTickingSounds.Length - 1)
            {
                _soundIndex = 0;
            }
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;

            if (_lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private UiScreen _screen;
        private MatchStartElement _element;

        private float _lifetime;

        private void Awake()
        {
            createScreen();
            createElement();
        }

        private void createScreen()
        {
            _screen = UiFactory.createUiScreen("Screen", ScreenOrder.startMatchScreen, transform);
        }

        private void createElement()
        {
            _element = UiFactory.createUiElement<MatchStartElement>("Element", _screen);
        }
    }
}
