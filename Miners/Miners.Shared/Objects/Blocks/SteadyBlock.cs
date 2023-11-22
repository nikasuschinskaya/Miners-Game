using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class SteadyBlock : Block
    {
        public override string Type => nameof(SteadyBlock);

        public SteadyBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
