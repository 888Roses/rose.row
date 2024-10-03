using rose.row.data;

namespace rose.row.easy_package.audio
{
    public static class AudioRegistry
    {
        public static string audioRoot => $"{Constants.basePath}/Audio";

        // TODO: Change this whenever I'll implement vehicles!
        public static readonly LocalAudioHolder hornTemp = new LocalAudioHolder("vehicles/horn_temp.wav");

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

        public static readonly LocalAudioHolder win = new LocalAudioHolder("yippeeeeeeeeeeeeee.mp3");
        public static readonly LocalAudioHolder capturePointNeutralized = new LocalAudioHolder("capture_point/capture_point_neutralized.wav");
        public static readonly LocalAudioHolder capturePointGM = new LocalAudioHolder("capture_point/capture_point_gm.wav");
        public static readonly LocalAudioHolder capturePointUS = new LocalAudioHolder("capture_point/capture_point_us.wav");
        public static readonly LocalAudioHolder capturePointRU = new LocalAudioHolder("capture_point/capture_point_ru.wav");
    }
}
