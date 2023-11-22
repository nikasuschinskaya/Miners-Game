using OpenTK;

namespace Miners.Shared.Objects.Base
{
    public abstract class GameObject : IGameObject
    {
        public Vector2 Position { get; set; }
        public string Path { get; set; }
        public abstract string Type { get; }

        protected GameObject(Vector2 position, string path)
        {
            Position = position;
            Path = path;
        }
    }
}