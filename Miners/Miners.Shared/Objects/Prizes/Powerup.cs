using Miners.Shared.Objects.Prizes.Base;
using OpenTK;

namespace Miners.Shared.Objects.Prizes
{
    public class Powerup : Prize
    {
        public override string Type => nameof(Powerup);

        public Powerup(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
