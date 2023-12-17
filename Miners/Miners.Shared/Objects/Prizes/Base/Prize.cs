using Miners.Shared.Objects.Base;
using OpenTK;

namespace Miners.Shared.Objects.Prizes.Base
{
    public abstract class Prize : GameObject
    {

        /// <summary>Initializes a new instance of the <see cref="Prize" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public Prize(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
