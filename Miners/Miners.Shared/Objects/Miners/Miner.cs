using Miners.Shared.Objects.Base;
using OpenTK;
using OpenTK.Input;
using System;

namespace Miners.Shared.Objects.Miners
{
    public class Miner : GameObject
    {
        private Vector2 _oldPosition;

        /// <summary>Gets or sets the speed.</summary>
        /// <value>The speed.</value>
        public float Speed { get; protected set; }

        /// <inheritdoc />
        public override string Type => nameof(Miner);

        /// <summary>Initializes a new instance of the <see cref="Miner" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        /// <param name="speed">The speed.</param>
        public Miner(Vector2 position, string path, float speed) : base(position, path) => Speed = speed;

        /// <summary>Sets the old position.</summary>
        public void SetOldPosition() => Position = _oldPosition;

        /// <summary>Sets the new position.</summary>
        /// <param name="position">The position.</param>
        public void SetNewPosition(Vector2 position) => Position = position;

        /// <summary>Gets the next position.</summary>
        /// <param name="keyboardState">State of the keyboard.</param>
        /// <param name="time">The time.</param>
        /// <param name="sameAsPrevious">if set to <c>true</c> [same as previous].</param>
        /// <returns>
        ///   <br />
        /// </returns>
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
    }
}