using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Enums;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.Blocks
{
    public class WeakResistantBlock : Block
    {
        public WeakResistantBlock(Vector2 position, Texture2D sprite) : base(position, sprite)
        {
        }
    }
}
