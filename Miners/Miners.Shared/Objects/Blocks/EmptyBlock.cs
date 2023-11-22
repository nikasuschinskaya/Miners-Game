using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class EmptyBlock : Block
    {
        public override string Type => nameof(EmptyBlock);

        public EmptyBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
