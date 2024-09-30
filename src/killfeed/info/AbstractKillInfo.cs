using rose.row.util;

namespace rose.row.killfeed.info
{
    public abstract class AbstractKillInfo : AbstractKillfeedInfo
    {
        public Actor source;
        public Actor killed;
        public DamageInfo damageInfo;
        public bool isSilentKill;

        public AbstractKillInfo(Actor source, Actor killed, DamageInfo damageInfo, bool isSilentKill) : base()
        {
            this.source = source;
            this.killed = killed;
            this.damageInfo = damageInfo;
            this.isSilentKill = isSilentKill;
        }

        public override bool compare(AbstractKillfeedInfo info)
        {
            if (info is AbstractKillInfo other)
            {
                if (info.GetType() != GetType())
                    return false;

                return other.killed != null && killed != null
                    && other.killed.getNameSafe() == killed.getNameSafe();
            }

            return false;
        }
    }
}