using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class MediumStableBlock : Block
    {
        public override string Type => nameof(MediumStableBlock);

        public MediumStableBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
