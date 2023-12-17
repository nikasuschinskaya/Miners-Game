using OpenTK;

namespace Miners.Shared.Objects.Base
{
    public interface IGameObject
    {
        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        Vector2 Position { get; set; }

        /// <summary>Gets or sets the path.</summary>
        /// <value>The path.</value>
        string Path { get; set; }

        /// <summary>Gets the type.</summary>
        /// <value>The type.</value>
        string Type { get; }
    }
}