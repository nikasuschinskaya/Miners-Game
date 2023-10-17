using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.BlocksFactories
{
    public class WeakResistantBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureWeakResistantBlockPath"].ToString();

        private int _x;
        private int _y;

        public WeakResistantBlockFactory(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override IBlock GetBlock()
        {
            return new WeakResistantBlock(new Vector2(_x, _y), TextureProcessing.LoadTexture(_texturePath));
        }
    }
}
