using Miners.Shared.Objects.Blocks.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks
{
    public class WeakResistantBlock : Block
    {
        public override string Type => nameof(WeakResistantBlock);

        public WeakResistantBlock(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
