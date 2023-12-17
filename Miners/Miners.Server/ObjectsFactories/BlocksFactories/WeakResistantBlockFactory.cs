using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.BlocksFactories
{
    public class WeakResistantBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureWeakResistantBlockPath"].ToString();

        /// <summary>Initializes a new instance of the <see cref="WeakResistantBlockFactory" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public WeakResistantBlockFactory(int x, int y) : base(x, y)
        {
        }

        /// <inheritdoc />
        public override IGameObject CreateObject() => 
            new WeakResistantBlock(new Vector2(X, Y), _texturePath);
    }
}
