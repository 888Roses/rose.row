using rose.row.data;

namespace rose.row.easy_package.audio
{
    public static class AudioRegistry
    {
        public static string audioRoot => $"{Constants.basePath}/Audio";

        public static readonly LocalAudioHolder[] startMatchTickingSounds = new LocalAudioHolder[]
        {
            new("ui/start_match/tick_0.wav"),
            new("ui/start_match/tick_1.wav"),
        };

        public static readonly LocalAudioHolder mouseHover = new LocalAudioHolder("ui/mouse_hover.wav");

        // TODO: Change this whenever I'll implement vehicles!
        public static readonly LocalAudioHolder hornTemp = new LocalAudioHolder("vehicles/horn_temp.wav");

        public static readonly LocalAudioHolder[] dieCrushed = new LocalAudioHolder[]
        {
            new LocalAudioHolder("die/crushed/die_crushed_01.wav"),
            new LocalAudioHolder("die/crushed/die_crushed_02.wav"),
            new LocalAudioHolder("die/crushed/die_crushed_03.wav"),
            new LocalAudioHolder("die/crushed/die_crushed_04.wav"),
        };

        public static readonly LocalAudioHolder[] dieFallBody = new LocalAudioHolder[]
        {
            new LocalAudioHolder("die/fall/body_die_fall_01.wav"),
            new LocalAudioHolder("die/fall/body_die_fall_02.wav"),
        };

        public static readonly LocalAudioHolder[] dieFall = new LocalAudioHolder[]
        {
            new LocalAudioHolder("die/fall/die_fall_01.wav"),
            new LocalAudioHolder("die/fall/die_fall_02.wav"),
            new LocalAudioHolder("die/fall/die_fall_03.wav"),
            new LocalAudioHolder("die/fall/die_fall_04.wav"),
            new LocalAudioHolder("die/fall/die_fall_05.wav"),
            new LocalAudioHolder("die/fall/die_fall_06.wav"),
        };

        public static readonly LocalAudioHolder[] hitPain = new LocalAudioHolder[]
        {
            new LocalAudioHolder("pain/hit_pain_01.wav"),
            new LocalAudioHolder("pain/hit_pain_02.wav"),
            new LocalAudioHolder("pain/hit_pain_03.wav"),
            new LocalAudioHolder("pain/hit_pain_04.wav"),
            new LocalAudioHolder("pain/hit_pain_05.wav"),
        };

        public static readonly LocalAudioHolder[] whistles = new LocalAudioHolder[]
        {
            new LocalAudioHolder("whistle/whistle_01.wav"),
            new LocalAudioHolder("whistle/whistle_02.wav"),
            new LocalAudioHolder("whistle/whistle_03.wav"),
            new LocalAudioHolder("whistle/whistle_04.wav"),
            new LocalAudioHolder("whistle/whistle_05.wav"),
        };

        public static readonly LocalAudioHolder win = new("yippeeeeeeeeeeeeee.mp3");
        public static readonly LocalAudioHolder capturePointNeutralizedFriendly = new("capture_point/capture_point_neutralized_friendly.wav");
        public static readonly LocalAudioHolder capturePointNeutralizedEnemy = new("capture_point/capture_point_neutralized_enemy.wav");
        public static readonly LocalAudioHolder capturePointGM = new("capture_point/capture_point_gm.wav");
        public static readonly LocalAudioHolder capturePointUS = new("capture_point/capture_point_us.wav");
        public static readonly LocalAudioHolder capturePointRU = new("capture_point/capture_point_ru.wav");
    }
}
