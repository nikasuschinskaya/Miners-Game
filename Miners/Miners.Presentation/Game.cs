using Miners.Presentation.Converters;
using Miners.Presentation.Render;
using Miners.Shared;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Miners;
using Miners.Shared.Objects.Prizes;
using Miners.Shared.Objects.Prizes.Base;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Miners.Presentation
{
    public class Game
    {
        private List<Miner> _allMiners;
        private List<Bomb> _bombs = new List<Bomb>();
        private Miner _miner;
        private Miner _otherMiner;
        private List<RectangleF> _notEmptyBlocksRectangles;
        private Texture2D _minerSprite;
        private RectangleF _minerRectangle;
        private List<Prize> _allBonuses = new List<Prize>();

        public IGameObject[,] Level { get; }
        public Miner Miner => _miner;

        public Game(IGameObject[,] level, int minerIndex)
        {
            Level = level;

            _allMiners = new List<Miner>();

            _notEmptyBlocksRectangles = new List<RectangleF>();

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
                    _notEmptyBlocksRectangles.Add(emptyBlockRectangle);
                }
            }

            _miner = _allMiners[minerIndex];
            _otherMiner = _allMiners[minerIndex == 0 ? 1 : 0];
            _minerSprite = TextureProcessing.LoadTexture(_miner.Path);

            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenServerSocket));
        }

        public void Update(double time)
        {
            KeyboardState kb = Keyboard.GetState();
            if (!kb.IsAnyKeyDown)
            {
                return;
            }
            int k = 1;

            var position = _miner.GetNextPosition(kb, time / k, out bool sameAsPrevious);
            if (!sameAsPrevious)
            {
                _minerRectangle = new RectangleF(
                    position.X * 48,
                    position.Y * 48,
                    _minerSprite.Width * 3,
                    _minerSprite.Height * 3);

                var json = JsonConvert.SerializeObject(_minerRectangle);

                Program.ClientSocket.Send(Encoding.UTF8.GetBytes($"{nameof(CommandType.MOVE)} {json}"));
            }

            if (kb.IsKeyDown(Key.Space))
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                Program.ClientSocket.Send(
                    Encoding.UTF8.GetBytes($"{nameof(CommandType.SPAWN_BOMB)} {JsonConvert.SerializeObject(position, settings)}"));
            }
            //_minerRectangle = new RectangleF(_miner.Position.X * 48, _miner.Position.Y * 48, _miner.Sprite.Width * 3, _miner.Sprite.Height * 3);

            //foreach (var emptyBlockRectangle in _emptyBlocksRectangle)
            //{
            //    if (CheckCollision(_minerRectangle, emptyBlockRectangle))
            //    {
            //        _miner.SetOldPosition();
            //        return;
            //    }
            //    else
            //    {
            //        //return;
            //    }
            //}


        }

        public void ListenServerSocket(object state)
        {
            while (true)
            {
                var buffer = new byte[1024 * 64];
                var bytes = Program.ClientSocket.Receive(buffer);
                var responseFromServer = Encoding.UTF8.GetString(buffer, 0, bytes);

                var index = responseFromServer.IndexOf(" ");
                if (index == -1) index = 0;
                var dataString = responseFromServer.Substring(index);
                if (responseFromServer.StartsWith(nameof(CommandType.MOVE_SELF)))
                {
                    var newPosition = JsonConvert.DeserializeObject<RectangleF>(dataString);
                    _miner.SetNewPosition(new Vector2(newPosition.X / 48, newPosition.Y / 48));
                    continue;
                }

                if (responseFromServer.StartsWith(nameof(CommandType.MOVE_OTHER)))
                {
                    var newPosition = JsonConvert.DeserializeObject<RectangleF>(dataString);
                    _otherMiner.SetNewPosition(new Vector2(newPosition.X / 48, newPosition.Y / 48));
                    continue;
                }

                if (responseFromServer.StartsWith(nameof(CommandType.SPAWN_BOMB)))
                {
                    var bomb = JsonConvert.DeserializeObject<Bomb>(dataString);
                    _bombs.Add(bomb);
                    continue;
                }

                if (responseFromServer.StartsWith(nameof(CommandType.EXPLORE_BOMB)))
                {
                    var sqrt = Math.Sqrt(2);
                    var bomb = _bombs[0];
                    for (int i = 0; i < bomb.Damage; i++)
                    {
                        for (var x = 0; x < Level.GetLength(0); x++)
                        {
                            for (var y = 0; y < Level.GetLength(1); y++)
                            {
                                if (Level[x, y] is null || Vector2.Distance(Level[x, y].Position, bomb.Position) > bomb.Radius * sqrt)
                                {
                                    continue;
                                }

                                if (Level[x, y] is SteadyBlock steadyBlock)
                                {
                                    Level[x, y] = new MediumStableBlock(steadyBlock.Position,
                                        ConfigurationManager.AppSettings["textureMediumStableBlockPath"].ToString());
                                    continue;
                                }

                                if (Level[x, y] is MediumStableBlock mediumStableBlock)
                                {
                                    Level[x, y] = new WeakResistantBlock(mediumStableBlock.Position,
                                        ConfigurationManager.AppSettings["textureWeakResistantBlockPath"].ToString());
                                    continue;
                                }

                                if (Level[x, y] is WeakResistantBlock weakResistantBlock)
                                {
                                    var removed = _notEmptyBlocksRectangles.RemoveAll(
                                        b => weakResistantBlock.Position.X * 48 == b.X &&
                                             weakResistantBlock.Position.Y * 48 == b.Y);
                                    Level[x, y] = new EmptyBlock(weakResistantBlock.Position, null);
                                    continue;
                                }
                            }
                        }
                    }
                    _bombs.RemoveAt(0);
                    continue;
                }

                if (responseFromServer.StartsWith(nameof(CommandType.YOUR_DEATH)))
                {
                    MessageBox.Show("You lost!");
                    Environment.Exit(0);
                }

                if (responseFromServer.StartsWith(nameof(CommandType.DRAW)))
                {
                    MessageBox.Show("The draw!");
                    Environment.Exit(0);
                }

                if (responseFromServer.StartsWith(nameof(CommandType.ENEMY_DEATH)))
                {
                    MessageBox.Show("You won!");
                    Environment.Exit(0);
                }

                if (responseFromServer.StartsWith(nameof(CommandType.SPAWN_BONUS)))
                {
                    var settings = new JsonSerializerSettings
                    {
                        Converters = { new GameObjectConverter() }
                    };
                    var newPosition = (Prize)JsonConvert.DeserializeObject<IGameObject>(dataString, settings);
                    _allBonuses.Add(newPosition);

                    continue;
                }

                if (responseFromServer.StartsWith(nameof(CommandType.YOU_GOT_BONUS)))
                {
                    var bonusPosition = JsonConvert.DeserializeObject<Vector2>(dataString);
                    var bonusesToApply = _allBonuses
                        .Where(b => b.Position == bonusPosition)
                        .ToList();

                    //foreach (var bonus in bonusesToApply)
                    //{
                    //    if (bonus is Letup letup)
                    //    {
                    //        continue;
                    //    }

                    //    if (bonus is Powerup powerup)
                    //    {
                    //        continue;
                    //    }
                    //}

                    foreach (var bonusToDelete in bonusesToApply)
                    {
                        lock (this)
                        {
                            _allBonuses.Remove(bonusToDelete);
                        }
                    }

                    continue;
                }

                if (responseFromServer.StartsWith(nameof(CommandType.ENEMY_GOT_BONUS)))
                {
                    var bonusPosition = JsonConvert.DeserializeObject<Vector2>(dataString);
                    lock (this)
                    {
                        _allBonuses.RemoveAll(b => b.Position == bonusPosition);
                    }

                    continue;
                }
            }
        }

        public void Render(/*double time*/)
        {
            if (Level == null)
            {
                return;
            }
            int width = Level.GetLength(0);
            int height = Level.GetLength(1); 
            float xOffset = 48f;
            float yOffset = 48f; 
            int zoom = 3;

            for (int i = 0; i < _bombs.Count; i++)
            {
                var bomb = _bombs[i]; 
                RenderGameObject(xOffset, yOffset, zoom, bomb);
            }
            for (int i = 0; i < _allBonuses.Count; i++)
            {
                var bonus = _allBonuses[i];
                RenderGameObject(xOffset, yOffset, zoom, bonus);
            }
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var block = Level[x, y]; 
                    if (block != null && block.Path != null)
                    {
                        RenderGameObject(xOffset, yOffset, zoom, block);
                    }
                }
            }
        }

        private static void RenderGameObject(float xOffset, float yOffset, int zoom, IGameObject gameObject)
        {           
            Texture2D sprite = TextureProcessing.LoadTexture(gameObject.Path);
            TextureRenderer.Draw(sprite, 
                                 new Vector2(gameObject.Position.X * xOffset, gameObject.Position.Y * yOffset),
                                 new Vector2(sprite.Width * zoom, sprite.Height * zoom));
        }


        //public void Render(/*double time*/)
        //{
        //    if (Level == null)
        //    {
        //        return;
        //    }

        //    int width = Level.GetLength(0);
        //    int height = Level.GetLength(1);
        //    float xOffset = 48f;
        //    float yOffset = 48f;
        //    int zoom = 3;
        //    foreach (var bomb in _bombs)
        //    {
        //        Texture2D sprite = TextureProcessing.LoadTexture(bomb.Path);
        //        TextureRenderer.Draw(sprite,
        //            new Vector2(bomb.Position.X * xOffset, bomb.Position.Y * yOffset),
        //            new Vector2(sprite.Width * zoom, sprite.Height * zoom));
        //    }
        //    lock (this)
        //    {
        //        foreach (var bonus in _allBonuses)
        //        {
        //            Texture2D sprite = TextureProcessing.LoadTexture(bonus.Path);
        //            TextureRenderer.Draw(sprite,
        //                new Vector2(bonus.Position.X * xOffset, bonus.Position.Y * yOffset),
        //                new Vector2(sprite.Width * zoom, sprite.Height * zoom));
        //        }
        //    }
        //    for (var x = 0; x < width; x++)
        //    {
        //        for (var y = 0; y < height; y++)
        //        {
        //            var block = Level[x, y];
        //            if (block != null && block.Path != null)
        //            {
        //                Texture2D sprite = TextureProcessing.LoadTexture(block.Path);
        //                TextureRenderer.Draw(sprite,
        //                    new Vector2(block.Position.X * xOffset, block.Position.Y * yOffset),
        //                    new Vector2(sprite.Width * zoom, sprite.Height * zoom));
        //            }
        //        }
        //    }
        //}
    }
}
