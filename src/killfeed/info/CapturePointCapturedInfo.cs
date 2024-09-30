namespace rose.row.killfeed.info
{
    /// <summary>
    /// An info called when capturing a point.
    /// </summary>
    public class CapturePointCapturedInfo : AbstractCapturePointActionInfo
    {
        public CapturePointCapturedInfo(SpawnPoint spawnPoint) : base(spawnPoint)
        {
        }

        public override string actionName => "Captured";

        public override int getXP() => 80;
    }
}