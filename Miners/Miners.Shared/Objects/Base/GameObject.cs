using OpenTK;

namespace Miners.Shared.Objects.Base
{
    public abstract class GameObject : IGameObject
    {
        /// <inheritdoc />
        public Vector2 Position { get; set; }

        /// <inheritdoc />
        public string Path { get; set; }

        /// <inheritdoc />
        public abstract string Type { get; }

        /// <summary>Initializes a new instance of the <see cref="GameObject" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        protected GameObject(Vector2 position, string path)
        {
            Position = position;
            Path = path;
        }
    }
}