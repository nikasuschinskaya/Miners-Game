using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class WeakResistantBlock : Block
    {
        /// <inheritdoc />
        public override string Type => nameof(WeakResistantBlock);


        /// <summary>Initializes a new instance of the <see cref="WeakResistantBlock" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public WeakResistantBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
