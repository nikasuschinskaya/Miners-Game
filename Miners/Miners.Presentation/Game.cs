using Miners.Presentation.Level;
using Miners.Presentation.Objects;
using Miners.Presentation.Objects.Blocks.Base;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Miners.Presentation
{
    public class Game
    {
        //private Miner _player;
        //private List<Mine> _mines;
        //private Random _random;
        private IBlock[,] _level;

        public Game()
        {
            //_player = new Miner { X = 100, Y = 100 };
            //_mines = new List<Mine>();
            //_random = new Random();
            var levelLoader = new LevelLoader();
            _level = levelLoader.LoadLevel();
        }

        public void Update()
        {
            // Обновление игры, например, обработка ввода и логика движения минёра.
            // Добавьте сюда код для обработки клавиш и перемещения игрока.
        }

        public void AddRandomMine()
        {
            //// Добавление случайной мины на поле.
            //int x = _random.Next(0, 800);
            //int y = _random.Next(0, 600);
            //var mine = new Mine { X = x, Y = y };
            //_mines.Add(mine);
        }

        public void Render(int screenWidth, int screenHeight)
        {
            if (_level != null)
            {
                int width = _level.GetLength(0);
                int height = _level.GetLength(1);

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0, screenWidth, screenHeight, 0, -1, 1);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        IBlock block = _level[x, y];
                        if (block != null)
                        {
                            DrawBlock(block);
                        }
                    }
                }
            }
        }

        private void DrawBlock(IBlock block)
        {
            if (block.Sprite == null) return;

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, block.Sprite.Id);

            GL.Begin(PrimitiveType.Quads);

            float xOffset = 32f;
            float yOffset = 32f;

            // Меняем позицию блока с учетом смещения относительно предыдущего блока
            float x = block.Position.X * xOffset;
            float y = block.Position.Y * yOffset;

            // Увеличиваем размер спрайта в 2 раза
            float width = block.Sprite.Width * 2;
            float height = block.Sprite.Height * 2;

            GL.TexCoord2(0f, 0f);
            GL.Vertex2(x, y);

            GL.TexCoord2(1f, 0f);
            GL.Vertex2(x + width, y);

            GL.TexCoord2(1f, 1f);
            GL.Vertex2(x + width, y + height);

            GL.TexCoord2(0f, 1f);
            GL.Vertex2(x, y + height);

            GL.End();

            GL.Disable(EnableCap.Texture2D);
        }
    }
}
