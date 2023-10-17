using Miners.Server.Objects.Blocks;
using Miners.Server.Objects.Blocks.Base;
using Miners.Server.Objects.BlocksFactories.Base;
using OpenTK;

namespace Miners.Server.Objects.BlocksFactories
{
    public class WeakResistantBlockFactory : BlockFactory
    {
        private int _x;
        private int _y;

        public WeakResistantBlockFactory(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override IBlock GetBlock() => new MediumStableBlock(new Vector2(_x, _y));
    }
}
