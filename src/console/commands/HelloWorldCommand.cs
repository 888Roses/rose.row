using rose.row.console.commands.arguments;
using UnityEngine;

namespace rose.row.console.commands
{
    public class HelloWorldCommand : AbstractConsoleCommand
    {
        public override string root => "hello.world";
        public override string description => "Throws an \"Hello, World!\" message in the console. Yipee!";

        public HelloWorldCommand()
        {
            arguments = new AbstractArgument[]
            {
                new IntArgument("count"),
            };
        }

        public override void execute()
        {
            var countArg = this["count"] as IntArgument;

            for (int i = 0; i < countArg.value; i++)
                Debug.Log($"Hello, World! {i}");
        }
    }
}