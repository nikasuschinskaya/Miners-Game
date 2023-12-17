using OpenTK.Graphics.OpenGL;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Miners.Presentation.Render
{
    public class TextureProcessing
    {
        private static readonly string _texturePath = ConfigurationManager.AppSettings["spritesPath"].ToString();

        /// <summary>Loads the texture.</summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException">Файл не найден, проверьте путь {path}</exception>
        public static Texture2D LoadTexture(string path)
        {
            if (!File.Exists(_texturePath + path))
            {
                throw new FileNotFoundException($"Файл не найден, проверьте путь {path}");
            }

            var id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            var bmp = new Bitmap(_texturePath + path);

            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                    ImageLockMode.ReadOnly,
                                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(target: TextureTarget.Texture2D,
                          level: 0,
                          internalformat: PixelInternalFormat.Rgba,
                          width: data.Width,
                          height: data.Height,
                          border: 0,
                          format: OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                          type: PixelType.UnsignedByte,
                          pixels: data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return new Texture2D(id, bmp.Width, bmp.Height);
        }
    }
}
