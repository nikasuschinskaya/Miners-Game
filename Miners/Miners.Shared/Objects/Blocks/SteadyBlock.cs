using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class SteadyBlock : Block
    {
        /// <inheritdoc />
        public override string Type => nameof(SteadyBlock);


        /// <summary>Initializes a new instance of the <see cref="SteadyBlock" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public SteadyBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
