using Miners.Server.Objects.Blocks.Enums;
using Miners.Server.Render;
using OpenTK;

namespace Miners.Server.Objects.Blocks.Base
{
    public interface IBlock
    {
        Vector2 Position { get; set; }
        Texture2D Sprite { get; set; }
        BlockType BlockType { get; set; }
    }
}
