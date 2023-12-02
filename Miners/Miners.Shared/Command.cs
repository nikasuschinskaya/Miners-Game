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
    public enum CommandType
    {
        OK,
        FAIL,
        MAP,

        MOVE,
        MOVE_SELF,
        MOVE_OTHER,
        NAME,
        SPAWN_BOMB,
        EXPLORE_BOMB,
        YOUR_DEATH,
        ENEMY_DEATH,
        DRAW,
        SPAWN_BONUS,
        YOU_GOT_BONUS,
        ENEMY_GOT_BONUS
    }
}