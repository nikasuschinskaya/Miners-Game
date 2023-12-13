using Miners.Shared.Objects.Bombs;

namespace Miners.Shared.Objects.Prizes.Base
{
    public abstract class BombDecorator : IBomb
    {
        protected readonly IBomb _bomb;

        public BombDecorator(IBomb bomb) => _bomb = bomb;

        public virtual int Radius
        {
            get => _bomb.Radius;
            set => _bomb.Radius = value;
        }

        public virtual int Damage
        {
            get => _bomb.Damage;
            set => _bomb.Damage = value;
        }
    }
}