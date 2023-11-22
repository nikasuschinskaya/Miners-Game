using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Prizes;
using Miners.Presentation.Objects.PrizesFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.PrizesFactories
{
    public class PowerupFactory : PrizeFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["texturePowerup"].ToString();
        public PowerupFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject()
        {
            return new Powerup(new Vector2(X, Y), TextureProcessing.LoadTexture(_texturePath));
        }
    }
}
