using Miners.Presentation.Enums;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Render;
using OpenTK;
using OpenTK.Input;
using System;
using System.Configuration;
using System.Net.PeerToPeer.Collaboration;

namespace Miners.Presentation.Objects
{
    public class Miner : Block
    {
        public int Health { get; protected set; }
        public float Speed { get; private set; }

        public Miner(Vector2 position, Texture2D sprite, float speed) : base(position, sprite)
        {
            Health = 10;
            Speed = speed;
        }

        public void Move(KeyboardState keyboardState, double time)
        {
            float x = Position.X;
            float y = Position.Y;

            if (keyboardState.IsKeyDown(Key.W))
            {
                y -= (float)(Speed * time);
            }
            else if (keyboardState.IsKeyDown(Key.S))
            {
                y += (float)(Speed * time);
            }

            if (keyboardState.IsKeyDown(Key.A))
            {
                x -= (float)(Speed * time);
            }
            else if (keyboardState.IsKeyDown(Key.D))
            {
                x += (float)(Speed * time);
            }

            Position = new Vector2(x, y);
        }

        //public void Move(Direction direction, double time)
        //{
        //    float x = Position.X;
        //    float y = Position.Y;

        //    switch (direction)
        //    {
        //        case Direction.Up:
        //            y += (float)(Math.Pow(Speed, 2) * time);
        //            break;

        //        case Direction.Down:
        //            y -= (float)(Math.Pow(Speed, 2) * time);
        //            break;

        //        case Direction.Left:
        //            x -= (float)(Math.Pow(Speed, 2) * time);
        //            break;

        //        case Direction.Right:
        //            x += (float)(Math.Pow(Speed, 2) * time);
        //            break;

        //        default:
        //            break;
        //    }

        //    //Transform.Position = new Vector2(x, y);
        //    Position = new Vector2(x, y);
        //}

    }
}
