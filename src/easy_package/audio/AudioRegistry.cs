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

        public static readonly LocalAudioHolder[] die = new LocalAudioHolder[]
        {
            new LocalAudioHolder("die/default/die_01.wav"),
            new LocalAudioHolder("die/default/die_02.wav"),
            new LocalAudioHolder("die/default/die_03.wav"),
            new LocalAudioHolder("die/default/die_04.wav"),
            new LocalAudioHolder("die/default/die_05.wav"),
            new LocalAudioHolder("die/default/die_06.wav"),
        };

        public static readonly LocalAudioHolder[] dieFallBody = new LocalAudioHolder[]
        {
            new LocalAudioHolder("die/fall/body_die_fall_01.wav"),
            new LocalAudioHolder("die/fall/body_die_fall_02.wav"),
        };

        public static readonly LocalAudioHolder[] hitPain = new LocalAudioHolder[]
        {
            new LocalAudioHolder("pain/hit/hit_pain_01.wav"),
            new LocalAudioHolder("pain/hit/hit_pain_02.wav"),
            new LocalAudioHolder("pain/hit/hit_pain_03.wav"),
            new LocalAudioHolder("pain/hit/hit_pain_04.wav"),
            new LocalAudioHolder("pain/hit/hit_pain_05.wav"),
        };

        public static readonly LocalAudioHolder[] fpsPain = new LocalAudioHolder[]
        {
            new("pain/fps/fps_pain_01.wav"),
            new("pain/fps/fps_pain_02.wav"),
            new("pain/fps/fps_pain_03.wav"),
            new("pain/fps/fps_pain_04.wav"),
            new("pain/fps/fps_pain_05.wav"),
            new("pain/fps/fps_pain_06.wav"),
            new("pain/fps/fps_pain_07.wav"),
            new("pain/fps/fps_pain_08.wav"),
            new("pain/fps/fps_pain_09.wav"),
            new("pain/fps/fps_pain_10.wav"),
            new("pain/fps/fps_pain_11.wav"),
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
