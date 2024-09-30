namespace rose.row.killfeed.info
{
    public abstract class AbstractCapturePointActionInfo : AbstractCapturePointInfo
    {
        protected AbstractCapturePointActionInfo(SpawnPoint spawnPoint) : base(spawnPoint)
        {
        }

        public abstract string actionName { get; }

        public override bool compare(AbstractKillfeedInfo info)
        {
            return false;
        }

        public override string getMessage(int xp)
        {
            var name = KillInfoUtil.yellow(actionName);
            var xpMessage = KillInfoUtil.text($" {xp} XP");
            return name.append(xpMessage).getString();
        }
    }
}