using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class EmptyBlock : Block
    {
        /// <inheritdoc />
        public override string Type => nameof(EmptyBlock);

        /// <summary>Initializes a new instance of the <see cref="EmptyBlock" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public EmptyBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
