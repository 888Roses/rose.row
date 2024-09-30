namespace rose.row.killfeed.info
{
    public abstract class AbstractCapturePointInfo : AbstractKillfeedInfo
    {
        public SpawnPoint spawnPoint;

        public AbstractCapturePointInfo(SpawnPoint spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
    }
}