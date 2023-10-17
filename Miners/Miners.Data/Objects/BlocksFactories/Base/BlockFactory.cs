using Miners.Server.Objects.Blocks.Base;

namespace Miners.Server.Objects.BlocksFactories.Base
{
    public abstract class BlockFactory
    {
        public abstract IBlock GetBlock();
    }
}
