namespace rose.row.killfeed.info
{
    /// <summary>
    /// An info called when neutralizing an enemy point.
    /// </summary>
    public class CapturePointNeutralizedInfo : AbstractCapturePointActionInfo
    {
        public CapturePointNeutralizedInfo(SpawnPoint spawnPoint) : base(spawnPoint)
        {
        }

        public override string actionName => "Neutralized";

        public override int getXP() => 40;
    }
}