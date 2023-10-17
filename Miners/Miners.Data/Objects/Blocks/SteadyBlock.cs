using Miners.Server.Objects.Blocks.Base;
using Miners.Server.Objects.Blocks.Enums;
using Miners.Server.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Server.Objects.Blocks
{
    public class SteadyBlock : IBlock
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureSteadyBlockPath"].ToString();
        public Vector2 Position { get; set; }
        public Texture2D Sprite { get; set; }
        public BlockType BlockType { get; set; }

        public SteadyBlock(Vector2 position)
        {
            Position = position;
            Sprite = TextureProcessing.LoadTexture(_texturePath);
            BlockType = BlockType.SteadyBlock;
        }
    }
}
