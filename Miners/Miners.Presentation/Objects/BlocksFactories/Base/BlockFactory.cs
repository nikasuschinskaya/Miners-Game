using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Presentation.Objects.BlocksFactories.Base
{
    public class BlockFactory : GameObjectFactory
    {
        //private int _x;
        //private int _y;

        public int X { get; set; }
        public int Y { get; set; }

        public BlockFactory(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override IGameObject CreateObject()
        {
            return new Block(new Vector2(X, Y), null);
        }
    }
}
