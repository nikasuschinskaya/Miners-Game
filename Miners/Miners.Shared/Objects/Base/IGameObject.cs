using OpenTK;

namespace Miners.Shared.Objects.Base
{
    public interface IGameObject
    {
        Vector2 Position { get; set; }
        string Path { get; set; }
        string Type { get; }
    }
}