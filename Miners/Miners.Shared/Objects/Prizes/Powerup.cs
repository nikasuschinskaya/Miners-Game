using Miners.Shared.Objects.Prizes.Base;
using OpenTK;

namespace Miners.Shared.Objects.Prizes
{
    public class Powerup : Prize
    {
        /// <inheritdoc />
        public override string Type => nameof(Powerup);

        /// <summary>Initializes a new instance of the <see cref="Powerup" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public Powerup(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
