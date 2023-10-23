using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.BlocksFactories
{
    public class MediumStableBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureMediumStableBlockPath"].ToString();

        public MediumStableBlockFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject()
        {
            return new MediumStableBlock(new Vector2(X, Y), TextureProcessing.LoadTexture(_texturePath));
        }
    }
}
