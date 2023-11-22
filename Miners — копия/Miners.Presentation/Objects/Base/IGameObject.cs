using Miners.Presentation.Render;
using OpenTK;

namespace Miners.Presentation.Objects.Base
{
    public interface IGameObject
    {
        Vector2 Position { get; set; }
        Texture2D Sprite { get; set; }
    }
}