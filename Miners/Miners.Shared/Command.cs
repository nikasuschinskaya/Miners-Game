using System.Collections.Generic;

namespace Miners.Shared
{
    public class Command
    {
        public static readonly Command NameCommand = new Command("Name");

        public string Name { get; }

        public Command(string name) => Name = name;

        public static IEnumerable<Command> GetCommands()
        {
            yield return NameCommand;
        }
    }
}