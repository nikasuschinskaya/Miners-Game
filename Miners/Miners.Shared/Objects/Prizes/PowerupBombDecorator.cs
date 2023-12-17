using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Prizes.Base;

namespace Miners.Shared.Objects.Prizes
{
    public class PowerupBombDecorator : BombDecorator
    {
        private const int Amount = 1;

        /// <summary>Initializes a new instance of the <see cref="PowerupBombDecorator" /> class.</summary>
        /// <param name="bomb">The bomb.</param>
        public PowerupBombDecorator(IBomb bomb) : base(bomb) { }

        /// <inheritdoc />
        public override int Damage
        {
            get => _bomb.Damage + Amount > 3 ? 3 : _bomb.Damage + Amount;
            set => _bomb.Damage = value;
        }
    }
}
