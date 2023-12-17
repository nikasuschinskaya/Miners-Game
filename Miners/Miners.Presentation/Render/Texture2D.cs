namespace Miners.Presentation.Render
{
    public class Texture2D
    {
        private readonly int _id;
        private readonly int _width;
        private readonly int _height;

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id => _id;

        /// <summary>Gets the width.</summary>
        /// <value>The width.</value>
        public int Width => _width;

        /// <summary>Gets the height.</summary>
        /// <value>The height.</value>
        public int Height => _height;

        /// <summary>Initializes a new instance of the <see cref="Texture2D" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Texture2D(int id, int width, int height)
        {
            _id = id;
            _width = width;
            _height = height;
        }
    }
}
