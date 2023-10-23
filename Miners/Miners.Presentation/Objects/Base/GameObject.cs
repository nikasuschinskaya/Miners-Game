using Miners.Presentation.Render;
using OpenTK;

namespace Miners.Presentation.Objects.Base
{
    public abstract class GameObject : IGameObject
    {
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; set; }

        protected GameObject(Vector2 position, Texture2D sprite)
        {
            Position = position;
            Sprite = sprite;
        }
    }
}
