using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;

namespace Miners.Server.ObjectsFactories.BlocksFactories
{
    public class EmptyBlockFactory : BlockFactory
    {
        /// <summary>Initializes a new instance of the <see cref="EmptyBlockFactory" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public EmptyBlockFactory(int x, int y) : base(x, y)
        {
        }

        /// <inheritdoc />
        public override IGameObject CreateObject() => 
            new EmptyBlock(new Vector2(X, Y), null);
    }
}
