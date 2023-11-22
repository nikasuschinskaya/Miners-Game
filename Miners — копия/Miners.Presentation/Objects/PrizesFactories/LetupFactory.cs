using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Prizes;
using Miners.Presentation.Objects.PrizesFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.PrizesFactories
{
    public class LetupFactory : PrizeFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureLetup"].ToString();
        public LetupFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject()
        {
            return new Letup(new Vector2(X, Y), TextureProcessing.LoadTexture(_texturePath));
        }
    }
}
