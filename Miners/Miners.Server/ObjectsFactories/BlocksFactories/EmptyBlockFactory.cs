using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;

namespace Miners.Server.ObjectsFactories.BlocksFactories
{
    public class EmptyBlockFactory : BlockFactory
    {
        public EmptyBlockFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject() => 
            new EmptyBlock(new Vector2(X, Y), null);
    }
}
