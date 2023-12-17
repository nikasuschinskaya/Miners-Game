using Miners.Shared.Objects.Base;

namespace Miners.Server.ObjectsFactories.Base
{
    public abstract class GameObjectFactory
    {

        /// <summary>Creates the object.</summary>
        /// <returns>
        /// Created game object
        /// </returns>
        public abstract IGameObject CreateObject();
    }
}
