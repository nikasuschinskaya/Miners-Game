using Miners.Shared.Objects.Base;
using OpenTK;

namespace Miners.Shared.Objects.Blocks.Base
{
    public abstract class Block : GameObject
    {
        public Block(Vector2 position, string path) : base(position, path)
        {
        }
    }
}
