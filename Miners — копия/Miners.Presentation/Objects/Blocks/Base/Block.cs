using Miners.Presentation.Objects.Base;
using Miners.Presentation.Render;
using OpenTK;

namespace Miners.Presentation.Objects.Blocks.Base
{
    public class Block : GameObject
    {
        public Block(Vector2 position, Texture2D sprite) : base(position, sprite)
        {
        }
    }
}
