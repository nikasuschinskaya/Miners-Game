using Miners.Server.ObjectsFactories.Base;

namespace Miners.Server.ObjectsFactories.PrizesFactories.Base
{
    public abstract class PrizeFactory : GameObjectFactory
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PrizeFactory(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
