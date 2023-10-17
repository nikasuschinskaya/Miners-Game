using Miners.Presentation.Enums;
using Miners.Presentation.Render;
using OpenTK;

namespace Miners.Presentation.Objects.Blocks.Base
{
    public interface IBlock
    {
        Vector2 Position { get; set; }
        Texture2D Sprite { get; set; }
    }
}
