namespace Miners.Engine.Render
{
    public class Texture2D
    {
        private readonly int _id;
        private readonly int _width;
        private readonly int _height;

        public int Id => _id;
        public int Width => _width;
        public int Height => _height;

        public Texture2D(int id, int width, int height)
        {
            _id = id;
            _width = width;
            _height = height;
        }
    }
}
