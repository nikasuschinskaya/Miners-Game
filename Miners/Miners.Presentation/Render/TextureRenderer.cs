using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Miners.Presentation.Render
{
    public static class TextureRenderer
    {
        public static void Draw(Texture2D texture, /*Transform transform*/Vector2 position, Vector2 scale)
        {
            if (texture == null) return;

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture.Id);

            GL.Begin(PrimitiveType.Quads);

            var vertices = new Vector2[4]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                new Vector2(0f, 1f)
            };

            for (int i = 0; i < vertices.Length; i++)
            {
                var vertex = vertices[i];

                GL.TexCoord2(vertex);
                //GL.Vertex2(transform.Position.X + vertex.X * transform.Scale.X,
                //           transform.Position.Y + vertex.Y * transform.Scale.Y);
                GL.Vertex2(position.X + vertex.X * scale.X,
                           position.Y + vertex.Y * scale.Y);
            }

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }


        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.LightGreen);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, screenWidth, screenHeight, 0, -1, 1);
        }
    }
}
