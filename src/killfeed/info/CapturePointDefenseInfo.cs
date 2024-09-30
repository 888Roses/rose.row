namespace rose.row.killfeed.info
{
    /// <summary>
    /// An info called when killing an enemy who's trying to capture on of your point.
    /// For now, you and the enemy have to be in the capture point. (This is correct)
    /// </summary>
    public class CapturePointDefenseInfo : AbstractKillInfo
    {
        public CapturePointDefenseInfo(Actor source,
                                       Actor killed,
                                       DamageInfo damageInfo,
                                       bool isSilentKill)
            : base(source, killed, damageInfo, isSilentKill) { }

        public override string getMessage(int experience)
        {
            var name = KillInfoUtil.yellow("Defense");
            var xp = KillInfoUtil.text($" {experience} XP");
            return name.append(xp).getString();
        }

        public override int getXP() => 60;
    }
}