using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.BlocksFactories
{
    public class SteadyBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureSteadyBlockPath"].ToString();

        public SteadyBlockFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject()
        {
            return new SteadyBlock(new Vector2(X, Y), _texturePath);
        }
    }
}
