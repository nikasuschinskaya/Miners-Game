using Miners.Presentation.Render;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Miners;
using Miners.Shared.Objects.Mines;
using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;
using System.Drawing;

namespace Miners.Presentation
{
    public class Game
    {
        private readonly List<Miner> _allMiners;
        private readonly Miner _miner;
        //private readonly List<Mine> _mines;
        private readonly List<RectangleF> _emptyBlocksRectangle;
        private RectangleF _minerRectangle;

        public IGameObject[,] Level { get; }

        public Game(IGameObject[,] level, int minerIndex)
        {
            Level = level;

            _allMiners = new List<Miner>();

            _emptyBlocksRectangle = new List<RectangleF>();

            for (var x = 0; x < Level.GetLength(0); x++)
            {
                for (var y = 0; y < Level.GetLength(1); y++)
                {
                    if (Level[x, y] is Miner miner)
                    {
                        _allMiners.Add(miner);
                        continue;
                    }
                    else if (Level[x, y] is EmptyBlock)
                    {
                        continue;
                    }

                    var emptyBlockRectangle = new RectangleF(Level[x, y].Position.X * 48, Level[x, y].Position.Y * 48, 48, 48);
                    _emptyBlocksRectangle.Add(emptyBlockRectangle);
                }
            }

            _miner = _allMiners[minerIndex];
            //_mines = new List<Mine>();
        }

        public void Update(double time)
        {
            KeyboardState kb = Keyboard.GetState();
            if (!kb.IsAnyKeyDown)
            {
                return;
            }
            int k = 3;

            _miner.Move(kb, time / k);

            Texture2D minerSprite = TextureProcessing.LoadTexture(_miner.Path);
            _minerRectangle = new RectangleF(_miner.Position.X * 48, _miner.Position.Y * 48, minerSprite.Width * 3, minerSprite.Height * 3);
            //_minerRectangle = new RectangleF(_miner.Position.X * 48, _miner.Position.Y * 48, _miner.Sprite.Width * 3, _miner.Sprite.Height * 3);

            foreach (var emptyBlockRectangle in _emptyBlocksRectangle)
            {
                if (CheckCollision(_minerRectangle, emptyBlockRectangle))
                {
                    _miner.SetOldPosition();
                    return;
                }
                else
                {
                    //return;
                }
            }
            //if (kb.IsKeyDown(Key.Space))
            //{
            //    // Установка мины в текущей позиции минера
            //    PlaceMine(_miner.Position);
            //}
            

        }

        public void Render(double time)
        {
            if (Level == null)
            {
                return;
            }

            int width = Level.GetLength(0);
            int height = Level.GetLength(1);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var block = Level[x, y];
                    if (block != null && block.Path != null)
                    {
                        float xOffset = 48f;
                        float yOffset = 48f;
                        int zoom = 3;

                        Texture2D sprite = TextureProcessing.LoadTexture(block.Path);
                        TextureRenderer.Draw(sprite,
                                             new Vector2(block.Position.X * xOffset, block.Position.Y * yOffset),
                                             new Vector2(sprite.Width * zoom, sprite.Height * zoom));
                    }
                }
            }

            //// Отрисовка мин
            //foreach (var mine in _mines)
            //{
            //    Texture2D mineSprite = TextureProcessing.LoadTexture(mine.Path);
            //    //RectangleF mineRectangle = new RectangleF(mine.Position.X * 48, mine.Position.Y * 48, mineSprite.Width * 3, mineSprite.Height * 3);

            //    TextureRenderer.Draw(mineSprite, new Vector2(mine.Position.X * 48, mine.Position.Y * 48), new Vector2(mineSprite.Width * 3, mineSprite.Height * 3));
            //}
        }

        //private void PlaceMine(Vector2 position)
        //{
        //    // Создание новой мины
        //    Mine mine = new Mine(position, "Mine/bomb.png");
        //    // Добавление мины в список
        //    _mines.Add(mine);
        //}

        private bool CheckCollision(RectangleF firstGameObject, RectangleF secondGameObject) =>
            firstGameObject.IntersectsWith(secondGameObject);
    }
}
