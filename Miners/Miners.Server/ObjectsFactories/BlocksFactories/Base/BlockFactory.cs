using Miners.Server.ObjectsFactories.Base;

namespace Miners.Server.ObjectsFactories.BlocksFactories.Base
{
    public abstract class BlockFactory : GameObjectFactory
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BlockFactory(int x, int y)
        {
            X = x;
            Y = y;
        }

        //public override IGameObject CreateObject()
        //{
        //    return new Block(new Vector2(X, Y), null);
        //}
    }
}
