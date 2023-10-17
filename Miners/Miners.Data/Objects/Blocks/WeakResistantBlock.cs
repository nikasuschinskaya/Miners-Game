using Miners.Server.Objects.Blocks.Base;
using Miners.Server.Objects.Blocks.Enums;
using Miners.Server.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Server.Objects.Blocks
{
    public class WeakResistantBlock : IBlock
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureWeakResistantBlockPath"].ToString();
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; set; }
        public BlockType BlockType { get; set; }

        public WeakResistantBlock(Vector2 position)
        {
            Position = position;
            Sprite = TextureProcessing.LoadTexture(_texturePath);
            BlockType = BlockType.WeakResistantBlock;
        }
    }
}
