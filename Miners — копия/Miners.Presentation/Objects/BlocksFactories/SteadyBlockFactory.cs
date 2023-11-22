using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.BlocksFactories
{
    public class SteadyBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureSteadyBlockPath"].ToString();

        public SteadyBlockFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject()
        {
            return new SteadyBlock(new Vector2(X, Y), TextureProcessing.LoadTexture(_texturePath));
        }
    }
}
