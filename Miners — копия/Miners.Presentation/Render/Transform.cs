using OpenTK;

namespace Miners.Presentation.Render
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public Transform(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
        }
    }
}