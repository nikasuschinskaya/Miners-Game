using System.Collections.Generic;

namespace Miners.Shared
{
    public class Command
    {
        /// <summary>The name command</summary>
        public static readonly Command NameCommand = new Command("Name");

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Initializes a new instance of the <see cref="Command" /> class.</summary>
        /// <param name="name">The name.</param>
        public Command(string name) => Name = name;

        /// <summary>Gets the commands.</summary>
        /// <returns>
        /// List of commands
        /// </returns>
        public static IEnumerable<Command> GetCommands()
        {
            yield return NameCommand;
        }
    }
}