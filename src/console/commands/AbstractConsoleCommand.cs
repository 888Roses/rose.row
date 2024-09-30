using rose.row.console.commands.arguments;

namespace rose.row.console.commands
{
    public abstract class AbstractConsoleCommand
    {
        public abstract string root { get; }
        public abstract string description { get; }
        public AbstractArgument[] arguments;

        public AbstractArgument this[string name]
        {
            get
            {
                foreach (var arg in arguments)
                {
                    if (arg.name == name)
                        return arg;
                }

                return null;
            }
        }

        public abstract void execute();
    }
}