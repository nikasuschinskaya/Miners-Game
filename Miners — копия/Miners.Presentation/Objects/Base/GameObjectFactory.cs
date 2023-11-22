using Miners.Presentation.Objects.Base;

namespace Miners.Presentation.Objects.Base
{
    public abstract class GameObjectFactory
    {
        public abstract IGameObject CreateObject();
    }
}
