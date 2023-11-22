using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.BlocksFactories.Base;
using OpenTK;

namespace Miners.Presentation.Objects.BlocksFactories
{
    public class EmptyBlockFactory : BlockFactory
    {
        public EmptyBlockFactory(int x, int y) : base(x, y)
        {
        }
        public override IGameObject CreateObject()
        {
            return new EmptyBlock(new Vector2(X, Y), null);
        }
    }
}
