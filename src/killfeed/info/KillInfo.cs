using rose.row.data;
using rose.row.easy_package.ui.text;
using rose.row.util;

namespace rose.row.killfeed.info
{
    /// <summary>
    /// An info called when killing an enemy.
    /// </summary>
    public class KillInfo : AbstractKillInfo
    {
        public KillInfo(Actor source,
                        Actor killed,
                        DamageInfo damageInfo,
                        bool isSilentKill)
            : base(source, killed, damageInfo, isSilentKill) { }

        public override string getMessage(int experience)
        {
            var killedComponent = new TextComponent("Killed ").withStyle(TextStyle.empty.withColor(Colors.yellow));
            var nameComponent = new TextComponent(killed.getNameSafe()).withStyle(TextStyle.empty.withColor(Colors.red));
            var xpComponent = new TextComponent($" {experience} XP").withStyle(TextStyle.empty.withColor(Colors.text));

            return killedComponent.append(nameComponent).append(xpComponent).getString();
        }

        public override int getXP() => 5;

        public override bool compare(AbstractKillfeedInfo info)
        {
            return false;
        }
    }
}