using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Render;
using OpenTK;

namespace Miners.Presentation.Objects.Blocks
{
    public class EmptyBlock : Block
    {
        public EmptyBlock(Vector2 position, Texture2D sprite) : base(position, sprite)
        {
        }
    }
}
