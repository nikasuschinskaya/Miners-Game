using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class MediumStableBlock : Block
    {
        /// <inheritdoc />
        public override string Type => nameof(MediumStableBlock);

        /// <summary>Initializes a new instance of the <see cref="MediumStableBlock" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public MediumStableBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
