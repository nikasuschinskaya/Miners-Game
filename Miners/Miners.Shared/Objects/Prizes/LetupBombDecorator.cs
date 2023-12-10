using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Prizes.Base;

namespace Miners.Shared.Objects.Prizes
{
    //public class LetupBombDecorator : IBomb
    //{
    //    private const int Amount = 1;

    //    private readonly IBomb _bomb;

    //    public LetupBombDecorator(IBomb bomb) => _bomb = bomb;

    //    public int Radius { get => _bomb.Radius; set => _bomb.Radius = value; }
    //    public int Damage { get => _bomb.Damage == 1 ? 1 : _bomb.Damage - Amount; set => _bomb.Damage = value; }
    //}
    public class LetupBombDecorator : BombDecorator
    {
        private const int Amount = 1;

        public LetupBombDecorator(IBomb bomb) : base(bomb) { }

        public override int Damage
        {
            get => _bomb.Damage == 1 ? 1 : _bomb.Damage - Amount;
            set => _bomb.Damage = value;
        }
    }
}
