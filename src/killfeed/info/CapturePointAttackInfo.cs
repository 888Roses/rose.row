namespace rose.row.killfeed.info
{
    /// <summary>
    /// An info called when you kill an enemey in their point.
    /// Both you and the enemy have to be in the capture point.
    /// </summary>
    public class CapturePointAttackInfo : AbstractKillInfo
    {
        public CapturePointAttackInfo(Actor source,
                                       Actor killed,
                                       DamageInfo damageInfo,
                                       bool isSilentKill)
            : base(source, killed, damageInfo, isSilentKill) { }

        public override string getMessage(int xp)
        {
            var name = KillInfoUtil.yellow("Raiding");
            var xpMessage = KillInfoUtil.text($" {xp} XP");
            return name.append(xpMessage).getString();
        }

        public override int getXP() => 10;
    }
}