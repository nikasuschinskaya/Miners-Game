using Miners.Shared.Objects.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks.Base
{
    public abstract class Block : GameObject
    {
        /// <summary>Initializes a new instance of the <see cref="Block" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public Block(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
