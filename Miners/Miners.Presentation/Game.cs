using Miners.Presentation.Converters;
using Miners.Presentation.Render;
using Miners.Shared;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Miners;
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
using System.Threading.Tasks;
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

            Task.Run(() => ListenServerSocket(null));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ListenServerSocket));
        }

        public void Update(double time)
        {
            KeyboardState kb = Keyboard.GetState();
            if (!kb.IsAnyKeyDown)
            {
                return;
            }
            int k = 20;

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
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MaxDepth = 1,
                    ContractResolver = new IgnorePropertiesResolver("Length", "LengthFast", "LengthSquared", "PerpendicularRight", "PerpendicularLeft", "Yx")
                };

                Program.ClientSocket.Send(
                    Encoding.UTF8.GetBytes($"{nameof(CommandType.SPAWN_BOMB)} {JsonConvert.SerializeObject(position, settings)}"));
            }
        }

        private readonly CommandType[] _commandTypes = ((CommandType[])Enum.GetValues(typeof(CommandType)));

        public void ListenServerSocket(object state)
        {
            while (true)
            {
                var buffer = new byte[1024 * 64 * 100];
                var bytes = Program.ClientSocket.Receive(buffer);
                var responseFromServer = Encoding.UTF8.GetString(buffer, 0, bytes);
                responseFromServer = responseFromServer.Substring(0, responseFromServer.Length - 1);
                var index = responseFromServer.IndexOf(" ");
                if (index == -1) index = 0;
                var dataString = responseFromServer.Substring(index + 1);
                if (_commandTypes.Any(cmd => dataString.Contains(cmd.ToString())))
                {
                    var commands = responseFromServer.Split(';');
                    for (int i = 0; i < commands.Length; i++)
                    {
                        var indx = commands[i].IndexOf(" ");
                        if (indx == -1) continue;
                        var data = commands[i].Substring(indx + 1);
                        HandleServerRequest(commands[i], data);
                    }
                    continue;
                }
                HandleServerRequest(responseFromServer, dataString);
            }
        }

        private void HandleServerRequest(string responseFromServer, string dataString)
        {
            if (responseFromServer.StartsWith(nameof(CommandType.MOVE_SELF)))
            {
                var newPosition = JsonConvert.DeserializeObject<RectangleF>(dataString);
                _miner.SetNewPosition(new Vector2(newPosition.X / 48, newPosition.Y / 48));
                return;
            }

            if (responseFromServer.StartsWith(nameof(CommandType.MOVE_OTHER)))
            {
                var newPosition = JsonConvert.DeserializeObject<RectangleF>(dataString);
                _otherMiner.SetNewPosition(new Vector2(newPosition.X / 48, newPosition.Y / 48));
                return;
            }

            if (responseFromServer.StartsWith(nameof(CommandType.SPAWN_BOMB)))
            {
                var bomb = JsonConvert.DeserializeObject<Bomb>(dataString);
                _bombs.Add(bomb);
                return;
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
                return;
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

                return;
            }

            if (responseFromServer.StartsWith(nameof(CommandType.YOU_GOT_BONUS)))
            {
                var bonusPosition = JsonConvert.DeserializeObject<Vector2>(dataString);
                var bonusesToApply = _allBonuses
                    .Where(b => b.Position == bonusPosition)
                    .ToList();

                foreach (var bonusToDelete in bonusesToApply)
                {
                    lock (this)
                    {
                        _allBonuses.Remove(bonusToDelete);
                    }
                }

                return;
            }

            if (responseFromServer.StartsWith(nameof(CommandType.ENEMY_GOT_BONUS)))
            {
                var bonusPosition = JsonConvert.DeserializeObject<Vector2>(dataString);
                lock (this)
                {
                    _allBonuses.RemoveAll(b => b.Position == bonusPosition);
                }

                return;
            }
        }

        public void Render()
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
                if (bomb == null)
                {
                    continue;
                }
                RenderGameObject(xOffset, yOffset, zoom, bomb);
            }
            for (int i = 0; i < _allBonuses.Count; i++)
            {
                var bonus = _allBonuses[i];
                if (bonus == null)
                {
                    continue;
                }
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

        Dictionary<string, Texture2D> _sprites = new Dictionary<string, Texture2D>();

        private void RenderGameObject(float xOffset, float yOffset, int zoom, IGameObject gameObject)
        {
            if (!_sprites.TryGetValue(gameObject.Path, out var sprite))
            {
                sprite = TextureProcessing.LoadTexture(gameObject.Path);
                _sprites.Add(gameObject.Path, sprite);
            }

            TextureRenderer.Draw(sprite,
                                 new Vector2(gameObject.Position.X * xOffset, gameObject.Position.Y * yOffset),
                                 new Vector2(sprite.Width * zoom, sprite.Height * zoom));
        }
    }
}