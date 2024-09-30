namespace rose.row.killfeed.info
{
    public class SuicideInfo : AbstractKillInfo
    {
        public SuicideInfo(Actor source,
                            Actor killed,
                            DamageInfo damageInfo,
                            bool isSilentKill)
            : base(source, killed, damageInfo, isSilentKill)
        {
        }

        public override string getMessage(int experience)
        {
            var name = KillInfoUtil.yellow("Suicide ");
            var xp = KillInfoUtil.red($"{experience} XP");
            return name.append(xp).getString();
        }

        public override bool compare(AbstractKillfeedInfo info)
        {
            return false;
        }

        public override int getXP() => -1;
    }
}