using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Prizes.Base;
using OpenTK;

namespace Miners.Presentation.Objects.PrizesFactories.Base
{
    public class PrizeFactory : GameObjectFactory
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PrizeFactory(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override IGameObject CreateObject()
        {
            return new Prize(new Vector2(X, Y), null);
        }
    }
}
