using Miners.Presentation.Enums;
using Miners.Presentation.Level;
using Miners.Presentation.Objects;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Render;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Net.PeerToPeer.Collaboration;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Miners.Presentation
{
    public class Game
    {
        private IBlock[,] _level;


        public Game()
        {
            var levelLoader = new LevelLoader();    
            _level = levelLoader.LoadLevel();
        }

        public void Update(double time)
        {
            //KeyboardState kb = Keyboard.GetState();
            //_miner.Move(kb, e.Time);
        }

   


        public void Render(/*FrameEventArgs e*/double time)
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
                            float xOffset = 32f;
                            float yOffset = 32f;
                            int zoom = 2;
                            //var transform = new Transform(new Vector2(x * xOffset, y * yOffset),
                            //                              new Vector2(block.Sprite.Width * zoom, block.Sprite.Height * zoom));
                            
                            TextureRenderer.Draw(block.Sprite,
                                                 new Vector2(block.Position.X * xOffset, block.Position.Y * yOffset),
                                                 new Vector2(block.Sprite.Width * zoom, block.Sprite.Height * zoom));


                            if (block is Miner miner)
                            {
                                //miner.Position = transform.Position;
                                //FirstMinerControll(miner, e);
                                KeyboardState kb = Keyboard.GetState();
                                if (kb.IsAnyKeyDown)
                                {
                                    miner.Move(kb, time);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FirstMinerControll(Miner miner, FrameEventArgs e)
        {
            KeyboardState kb = Keyboard.GetState();

   
   



            //    //var scaleX = miner.Transform.Scale.X < 0 ? miner.Transform.Scale.X * -1 : miner.Transform.Scale.X;
            //    //var scaleY = miner.Transform.Scale.Y;

            //    //Vector2 oldPosition = miner.Transform.Position;

            //    if (kb.IsKeyDown(Key.W) ^ kb.IsKeyDown(Key.S))
            //    {
            //        if (kb.IsKeyDown(Key.W))
            //        {
            //            miner.Move(Direction.Up, e.Time);
            //        }
            //        else
            //        {
            //            miner.Move(Direction.Down, e.Time);
            //        }

            //        //if (CheckingColliders(submarine))
            //        //{
            //        //    submarine.Transform.Position = oldPosition;
            //        //}
            //    }

            //    if (kb.IsKeyDown(Key.A) ^ kb.IsKeyDown(Key.D))
            //    {
            //        if (kb.IsKeyDown(Key.A))
            //        {
            //            //miner.Transform.Scale = new Vector2(-scaleX, scaleY);
            //            miner.Move(Direction.Left, e.Time);
            //        }
            //        else
            //        {
            //            //miner.Transform.Scale = new Vector2(scaleX, scaleY);
            //            miner.Move(Direction.Right, e.Time);
            //        }

            //        //if (CheckingColliders(submarine))
            //        //{
            //        //    submarine.Transform.Position = oldPosition;
            //        //}
            //    }
        }

    }
}
