using Miners.Presentation.Render;
using OpenTK;

namespace Miners.Presentation.Objects.Blocks.Base
{
    public abstract class Block : IBlock
    {
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; set; }

        protected Block(Vector2 position, Texture2D sprite)
        {
            Position = position;
            Sprite = sprite;
        }
    }
}
