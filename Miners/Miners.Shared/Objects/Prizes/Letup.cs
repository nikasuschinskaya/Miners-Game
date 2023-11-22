using Miners.Shared.Objects.Prizes.Base;
using OpenTK;

namespace Miners.Shared.Objects.Prizes
{
    public class Letup : Prize
    {
        public override string Type => nameof(Letup);

        public Letup(Vector2 position, string path) : base(position, path)
        {
        }

    }
}
