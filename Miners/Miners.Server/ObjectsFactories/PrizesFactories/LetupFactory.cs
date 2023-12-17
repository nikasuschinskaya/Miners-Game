using Miners.Server.ObjectsFactories.PrizesFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Prizes;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.PrizesFactories
{
    public class LetupFactory : PrizeFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureLetup"].ToString();

        /// <summary>Initializes a new instance of the <see cref="LetupFactory" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public LetupFactory(int x, int y) : base(x, y)
        {
        }
        /// <inheritdoc />
        public override IGameObject CreateObject() => 
            new Letup(new Vector2(X, Y), _texturePath);
    }
}
