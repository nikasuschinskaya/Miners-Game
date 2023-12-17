using Miners.Shared.Objects.Prizes.Base;
using OpenTK;

namespace Miners.Shared.Objects.Prizes
{
    public class Letup : Prize
    {
        /// <inheritdoc />
        public override string Type => nameof(Letup);

        /// <summary>Initializes a new instance of the <see cref="Letup" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public Letup(Vector2 position, string path) : base(position, path)
        {
        }

    }
}
