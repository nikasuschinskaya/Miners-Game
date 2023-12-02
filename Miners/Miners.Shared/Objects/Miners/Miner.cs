using Miners.Shared.Objects.Base;
using OpenTK;
using OpenTK.Input;
using System;

namespace Miners.Shared.Objects.Miners
{
    public class Miner : GameObject
    {
        private Vector2 _oldPosition;

        //public int Health { get; protected set; }
        public float Speed { get; protected set; }

        public Miner(Vector2 position, string path, float speed) : base(position, path)
        {
            //Health = 10;
            Speed = speed;
        }

        public override string Type => nameof(Miner);

        public void SetOldPosition() => Position = _oldPosition;

        public void SetNewPosition(Vector2 position) => Position = position;

        public Vector2 GetNextPosition(KeyboardState keyboardState, double time, out bool sameAsPrevious)
        {
            float x = Position.X;
            float y = Position.Y;
            sameAsPrevious = true;
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (keyboardState.IsKeyDown(key))
                {
                    switch (key)
                    {
                        case Key.W:
                            y -= (float)(Speed * time);
                            sameAsPrevious = false;
                            break;
                        case Key.S:
                            y += (float)(Speed * time);
                            sameAsPrevious = false;
                            break;
                        case Key.A:
                            x -= (float)(Speed * time);
                            sameAsPrevious = false;
                            break;
                        case Key.D:
                            x += (float)(Speed * time);
                            sameAsPrevious = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            return new Vector2(x, y);
        }

        public void Move(KeyboardState keyboardState, double time)
        {
            float x = Position.X;
            float y = Position.Y;

            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (keyboardState.IsKeyDown(key))
                {
                    switch (key)
                    {
                        case Key.W:
                            y -= (float)(Speed * time);
                            break;
                        case Key.S:
                            y += (float)(Speed * time);
                            break;
                        case Key.A:
                            x -= (float)(Speed * time);
                            break;
                        case Key.D:
                            x += (float)(Speed * time);
                            break;
                        default:
                            break;
                    }
                }
            }


            //if (keyboardState.IsKeyDown(Key.W))
            //{
            //    y -= (float)(Speed * time);
            //}
            //else if (keyboardState.IsKeyDown(Key.S))
            //{
            //    y += (float)(Speed * time);
            //}

            //if (keyboardState.IsKeyDown(Key.A))
            //{
            //    x -= (float)(Speed * time);
            //}
            //else if (keyboardState.IsKeyDown(Key.D))
            //{
            //    x += (float)(Speed * time);
            //}
            _oldPosition = Position;
            Position = new Vector2(x, y);
        }
    }
}