using Miners.Server.CommandHandlers.Base;
using Miners.Shared;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Bombs;
using Newtonsoft.Json;
using OpenTK;
using System.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace Miners.Server.CommandHandlers
{
    public class BombCommandHandler : ICommandHandler
    {
        private static readonly string _texturePath = ConfigurationManager.AppSettings["textureMine"].ToString();

        //private readonly Game _game;
        private readonly Socket _userSocket;
        private readonly Socket _other;
        private readonly int _minerIndex;
        private readonly int _otherMinerIndex;

        public BombCommandHandler(/*Game game, */Socket userSocket, Socket other, int minerIndex)
        {
            //_game = game;
            _userSocket = userSocket;
            _other = other;
            _minerIndex = minerIndex;
            _otherMinerIndex = minerIndex == 0 ? 1 : 0;
        }

        private Bomb _bomb;

        public bool Handle(string request)
        {
            if (string.IsNullOrEmpty(request) || !request.StartsWith(nameof(CommandType.SPAWN_BOMB)))
            {
                return false;
            }

            if (_bomb != null)
            {
                return true;
            }

            var index = request.IndexOf(" ");
            var positionString = request.Substring(index);
            var bombPosition = JsonConvert.DeserializeObject<Vector2>(positionString);
            //var bomb = new Bomb(new Vector2(bombPosition.X, bombPosition.Y), _texturePath);
            var bombData = Game.Instance.GetBomb(_minerIndex);
            var bomb = new Bomb(new Vector2(bombPosition.X, bombPosition.Y), _texturePath);
            bomb.Damage = bombData.Damage;
            bomb.Radius = bombData.Radius;
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            _userSocket.Send(Encoding.UTF8.GetBytes($"{nameof(CommandType.SPAWN_BOMB)} {JsonConvert.SerializeObject(bomb, settings)}"));
            _other.Send(Encoding.UTF8.GetBytes($"{nameof(CommandType.SPAWN_BOMB)} {JsonConvert.SerializeObject(bomb, settings)}"));
            _bomb = bomb;
            var timer = new Timer(Bomb.TimeBeforeExpode * 1000);
            timer.AutoReset = false;
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            return true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            bool areYouDead = Vector2.Distance(Game.Instance.GetMiner(_minerIndex).Position, _bomb.Position) < _bomb.Radius * 1.41;
            bool isEnemyDead = Vector2.Distance(Game.Instance.GetMiner(_otherMinerIndex).Position, _bomb.Position) < _bomb.Radius * 1.41;
            if (areYouDead && isEnemyDead)
            {
                _userSocket.Send(Encoding.UTF8.GetBytes(nameof(CommandType.DRAW)));
                _other.Send(Encoding.UTF8.GetBytes(nameof(CommandType.DRAW)));
                return;
            }

            if (areYouDead)
            {
                _userSocket.Send(Encoding.UTF8.GetBytes(nameof(CommandType.YOUR_DEATH)));
                _other.Send(Encoding.UTF8.GetBytes(nameof(CommandType.ENEMY_DEATH)));
                return;
            }

            if (isEnemyDead)
            {
                _userSocket.Send(Encoding.UTF8.GetBytes(nameof(CommandType.ENEMY_DEATH)));
                _other.Send(Encoding.UTF8.GetBytes(nameof(CommandType.YOUR_DEATH)));
                return;
            }

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var response = $"{CommandType.EXPLORE_BOMB} {JsonConvert.SerializeObject(_bomb, settings)}";
            _userSocket.Send(Encoding.UTF8.GetBytes(response));
            _other.Send(Encoding.UTF8.GetBytes(response));

            for (var x = 0; x < Game.Instance.Level.GetLength(0); x++)
            {
                for (var y = 0; y < Game.Instance.Level.GetLength(1); y++)
                {
                    for (int i = 0; i < _bomb.Damage; i++)
                    {
                        if (Game.Instance.Level[x, y] is null || Vector2.Distance(Game.Instance.Level[x, y].Position, _bomb.Position) > _bomb.Radius * 1.41)
                        {
                            break;
                        }

                        if (Game.Instance.Level[x, y] is SteadyBlock steadyBlock)
                        {
                            Game.Instance.Level[x, y] = new MediumStableBlock(steadyBlock.Position, null);
                            continue;
                        }

                        if (Game.Instance.Level[x, y] is MediumStableBlock mediumStableBlock)
                        {
                            Game.Instance.Level[x, y] = new WeakResistantBlock(mediumStableBlock.Position, null);
                            continue;
                        }

                        if (Game.Instance.Level[x, y] is WeakResistantBlock weakResistantBlock)
                        {
                            Game.Instance.NotEmptyBlocksRectangles.RemoveAll(
                                b => weakResistantBlock.Position.X * 48 == b.X &&
                                     weakResistantBlock.Position.Y * 48 == b.Y);
                            Game.Instance.Level[x, y] = new EmptyBlock(weakResistantBlock.Position, null);
                            break;
                        }
                    }
                }
            }
            _bomb = null;
        }
    }
}
