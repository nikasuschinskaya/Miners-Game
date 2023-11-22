using Miners.Shared.Objects.Base;

namespace Miners.Server.ObjectsFactories.Base
{
    public abstract class GameObjectFactory
    {
        public abstract IGameObject CreateObject();
    }
}
