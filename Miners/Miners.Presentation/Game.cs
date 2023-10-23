using Miners.Presentation.Level;
using Miners.Presentation.Objects;
using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.Miners;
using Miners.Presentation.Render;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Miners.Presentation
{
    public class Game
    {
        private IGameObject[,] _level;
        private Miner _miner;
        private List<RectangleF> _emptyBlocksRectangle;
        //private Rectangle _emptyBlockRectangle;
        private RectangleF _minerRectangle;


        public Game()
        {
            var levelLoader = new LevelLoader();
            _level = levelLoader.LoadLevel();

            _emptyBlocksRectangle = new List<RectangleF>();

            for (var x = 0; x < _level.GetLength(0); x++)
            {
                for (var y = 0; y < _level.GetLength(1); y++)
                {
                    if (_level[x, y] is Miner miner)
                    {
                        _miner = miner;
                        continue;
                    }
                    else if (_level[x, y] is EmptyBlock)
                    {
                        continue;
                    }

                    var emptyBlockRectangle = new RectangleF(_level[x, y].Position.X * 48, _level[x, y].Position.Y * 48, 48, 48);
                    _emptyBlocksRectangle.Add(emptyBlockRectangle);
                }
            }
        }

        public void Update(double time)
        {
            KeyboardState kb = Keyboard.GetState();
            if (!kb.IsAnyKeyDown)
            {
                return;
            }
            int k = 3;

            _miner.Move(kb, time/ k);

            _minerRectangle = new RectangleF(_miner.Position.X * 48, _miner.Position.Y * 48, _miner.Sprite.Width * 3, _miner.Sprite.Height * 3);
            
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

            //_miner.Move(kb, time / k);

        }

        public void Render(double time)
        {

            if (_level != null)
            {
                int width = _level.GetLength(0);
                int height = _level.GetLength(1);

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var block = _level[x, y];
                        if (block != null && block.Sprite != null)
                        {
                            float xOffset = 48f;
                            float yOffset = 48f;
                            int zoom = 3;

                            TextureRenderer.Draw(block.Sprite,
                                                 new Vector2(block.Position.X * xOffset, block.Position.Y * yOffset),
                                                 new Vector2(block.Sprite.Width * zoom, block.Sprite.Height * zoom));
                        }
                    }
                }
            }
        }
        private bool CheckCollision(RectangleF firstGameObject, RectangleF secondGameObject) => 
            firstGameObject.IntersectsWith(secondGameObject);
    }
}
