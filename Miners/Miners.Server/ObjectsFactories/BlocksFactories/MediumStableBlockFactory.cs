using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.BlocksFactories
{
    public class MediumStableBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureMediumStableBlockPath"].ToString();

        /// <summary>Initializes a new instance of the <see cref="MediumStableBlockFactory" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public MediumStableBlockFactory(int x, int y) : base(x, y)
        {
        }

        /// <inheritdoc />
        public override IGameObject CreateObject() => 
            new MediumStableBlock(new Vector2(X, Y), _texturePath);
    }
}
