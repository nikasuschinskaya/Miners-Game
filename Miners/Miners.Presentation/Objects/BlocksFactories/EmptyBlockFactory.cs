using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories.Base;
using OpenTK;

namespace Miners.Presentation.Objects.BlocksFactories
{
    public class EmptyBlockFactory : BlockFactory
    {
        private int _x;
        private int _y;

        public EmptyBlockFactory(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override IBlock GetBlock()
        {
            return new EmptyBlock(new Vector2(_x, _y), null);
        }
    }
}
