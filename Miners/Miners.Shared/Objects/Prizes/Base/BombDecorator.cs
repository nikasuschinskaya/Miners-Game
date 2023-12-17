using Miners.Shared.Objects.Bombs;

namespace Miners.Shared.Objects.Prizes.Base
{
    public abstract class BombDecorator : IBomb
    {
        protected readonly IBomb _bomb;

        /// <summary>Initializes a new instance of the <see cref="BombDecorator" /> class.</summary>
        /// <param name="bomb">The bomb.</param>
        public BombDecorator(IBomb bomb) => _bomb = bomb;

        /// <inheritdoc />
        public virtual int Radius
        {
            get => _bomb.Radius;
            set => _bomb.Radius = value;
        }

        /// <inheritdoc />
        public virtual int Damage
        {
            get => _bomb.Damage;
            set => _bomb.Damage = value;
        }
    }
}