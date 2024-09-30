using rose.row.actor.health;
using rose.row.data;
using rose.row.easy_package.ui.text;
using rose.row.util;

namespace rose.row.killfeed.info
{
    /// <summary>
    /// An info called when you hurt an enemy.
    /// </summary>
    public class HurtInfo : AbstractKillInfo
    {
        public HurtInfo(Actor source,
                        Actor killed,
                        DamageInfo damageInfo,
                        bool isSilentKill)
            : base(source, killed, damageInfo, isSilentKill) { }

        public override string getMessage(int experience)
        {
            var killedComponent = new TextComponent("Wounded ").withStyle(TextStyle.empty.withColor(Colors.yellow));
            var nameComponent = new TextComponent(killed.getNameSafe()).withStyle(TextStyle.empty.withColor(Colors.text));

            var finalComponent = killedComponent.append(nameComponent);
            if (experience > 0)
            {
                var xpComponent = new TextComponent($" {experience} XP").withStyle(TextStyle.empty.withColor(Colors.text));
                finalComponent.append(xpComponent);
            }

            return finalComponent.getString();
        }

        // TODO: Implement health segments properly.
        public override int getXP()
        {
            if (damageInfo.healthDamage >= CustomActorHealth.getMaxHealthForHealthFloor(HealthFloor.Lowest))
                return 1;

            if (damageInfo.healthDamage >= CustomActorHealth.getMaxHealthForHealthFloor(HealthFloor.Middle))
                return 2;

            if (damageInfo.healthDamage >= CustomActorHealth.getMaxHealthForHealthFloor(HealthFloor.Highest))
                return 3;

            return 0;
        }
    }
}