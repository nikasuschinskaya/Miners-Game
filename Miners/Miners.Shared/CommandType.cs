namespace Miners.Shared
{
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