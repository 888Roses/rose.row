using rose.row.data;

namespace rose.row.easy_package.audio
{
    public static class AudioRegistry
    {
        public static string audioRoot => $"{Constants.basePath}/Audio";

        public static readonly LocalAudioHolder[] whistles = new LocalAudioHolder[]
        {
            new LocalAudioHolder("whistle/whistle_01.wav"),
            new LocalAudioHolder("whistle/whistle_02.wav"),
            new LocalAudioHolder("whistle/whistle_03.wav"),
            new LocalAudioHolder("whistle/whistle_04.wav"),
            new LocalAudioHolder("whistle/whistle_05.wav"),
        };

        public static readonly LocalAudioHolder capturePointNeutralized = new LocalAudioHolder("capture_point/capture_point_neutralized.wav");
        public static readonly LocalAudioHolder capturePointGM = new LocalAudioHolder("capture_point/capture_point_gm.wav");
        public static readonly LocalAudioHolder capturePointUS = new LocalAudioHolder("capture_point/capture_point_us.wav");
        public static readonly LocalAudioHolder capturePointRU = new LocalAudioHolder("capture_point/capture_point_ru.wav");
    }
}
