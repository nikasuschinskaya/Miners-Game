using Miners.Server.ObjectsFactories.Base;

namespace Miners.Server.ObjectsFactories.BlocksFactories.Base
{
    public abstract class BlockFactory : GameObjectFactory
    {
        /// <summary>Gets or sets the x.</summary>
        /// <value>The x.</value>
        public int X { get; set; }

        /// <summary>Gets or sets the y.</summary>
        /// <value>The y.</value>
        public int Y { get; set; }

        /// <summary>Initializes a new instance of the <see cref="BlockFactory" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public BlockFactory(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
