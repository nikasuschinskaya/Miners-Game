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

        public WeakResistantBlockFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject() => 
            new WeakResistantBlock(new Vector2(X, Y), _texturePath);
    }
}
