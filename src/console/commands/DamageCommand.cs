using rose.row.console.commands.arguments;

namespace rose.row.console.commands
{
    public class DamageCommand : AbstractConsoleCommand
    {
        public const string k_CommandName = "damage";
        public const string k_AmountArgument = "amount";

        public override string root => k_CommandName;
        public override string description => "Damages the player for the given amount.";

        public DamageCommand()
        {
            arguments = new AbstractArgument[]
            {
                new IntArgument(k_AmountArgument, 10)
            };
        }

        public override void execute()
        {
            var amountArgument = this[k_AmountArgument] as IntArgument;
            var amount = amountArgument.value;
            var damage = DamageInfo.Default;
            damage.healthDamage = amount;
            FpsActorController.instance.actor.Damage(damage);
        }
    }
}