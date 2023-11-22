using Miners.Shared.Objects.Base;
using OpenTK;

namespace Miners.Shared.Objects.Prizes.Base
{
    public abstract class Prize : GameObject
    {
        public Prize(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
