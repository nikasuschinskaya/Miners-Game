using Miners.Shared.Objects.Bombs;

namespace Miners.Shared.Objects.Prizes
{
    public class PowerupBombDecorator
    {
        private const int Amount = 1;

        private readonly IBomb _bomb;

        public PowerupBombDecorator(IBomb bomb) => _bomb = bomb;

        public int Radius { get => _bomb.Radius; set => _bomb.Radius = value; }
        public int Damage { get => _bomb.Damage + Amount > 3 ? 3 : _bomb.Damage + Amount; set => _bomb.Damage = value; }
    }
}
