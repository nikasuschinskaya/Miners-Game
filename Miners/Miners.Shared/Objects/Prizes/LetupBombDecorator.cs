using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Prizes.Base;

namespace Miners.Shared.Objects.Prizes
{
    public class LetupBombDecorator : BombDecorator
    {
        private const int Amount = 1;

        /// <summary>Initializes a new instance of the <see cref="LetupBombDecorator" /> class.</summary>
        /// <param name="bomb">The bomb.</param>
        public LetupBombDecorator(IBomb bomb) : base(bomb) { }

        /// <inheritdoc />
        public override int Damage
        {
            get => _bomb.Damage == 1 ? 1 : _bomb.Damage - Amount;
            set => _bomb.Damage = value;
        }
    }
}
