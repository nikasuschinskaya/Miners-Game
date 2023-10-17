using Miners.Presentation.Objects.Blocks.Base;

namespace Miners.Presentation.Objects.BlocksFactories.Base
{
    public abstract class BlockFactory
    {
        public abstract IBlock GetBlock();
    }
}
