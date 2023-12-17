using Miners.Server.CommandHandlers.Base;
using Miners.Shared;
using Miners.Shared.Objects.Prizes;
using Newtonsoft.Json;
using OpenTK;
using System.Drawing;
using System.Net.Sockets;
using System.Text;

namespace Miners.Server.CommandHandlers
{
    public class MoveCommandHandler : ICommandHandler
    {
        private readonly Socket _userSocket;
        private readonly Socket _otherSocket;
        private readonly int _minerIndex;

        /// <summary>Initializes a new instance of the <see cref="MoveCommandHandler" /> class.</summary>
        /// <param name="userSocket">The user socket.</param>
        /// <param name="other">The other.</param>
        /// <param name="minerIndex">Index of the miner.</param>
        public MoveCommandHandler(Socket userSocket, Socket other, int minerIndex)
        {
            _userSocket = userSocket;
            _otherSocket = other;
            _minerIndex = minerIndex;
        }

        /// <inheritdoc />
        public bool Handle(string request)
        {
            if (string.IsNullOrEmpty(request) || !request.StartsWith(nameof(CommandType.MOVE)))
            {
                return false;
            }

            var index = request.IndexOf(" ");
            var positionString = request.Substring(index);

            RectangleF newPosition = JsonConvert.DeserializeObject<RectangleF>(positionString);

            var result = Game.Instance.SetNewMinerPosition(newPosition, _minerIndex);
            if (result)
            {
                _userSocket.Send(Encoding.UTF8.GetBytes($"{nameof(CommandType.MOVE_SELF)} {positionString};"));
                _otherSocket.Send(Encoding.UTF8.GetBytes($"{nameof(CommandType.MOVE_OTHER)} {positionString};"));
            }

            CheckIfUserGotPrize();

            return true;
        }

        private void CheckIfUserGotPrize()
        {
            var allPrizes = Game.Instance.AllPrizes;
            var minerPosition = Game.Instance.GetMiner(_minerIndex).Position;
            for (int i = 0; i < allPrizes.Count; i++)
            {
                if (Vector2.Distance(minerPosition, allPrizes[i].Position) < 1)
                {
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        MaxDepth = 1,
                        ContractResolver = new IgnorePropertiesResolver("Length", "LengthFast", "LengthSquared", "PerpendicularRight", "PerpendicularLeft", "Yx")
                    };
                    var json = JsonConvert.SerializeObject(allPrizes[i].Position, settings);
                    if (allPrizes[i] is Letup)
                    {
                        Game.Instance.AllBombs[_minerIndex] = new LetupBombDecorator(Game.Instance.AllBombs[_minerIndex]);
                    }
                    if (allPrizes[i] is Powerup)
                    {
                        Game.Instance.AllBombs[_minerIndex] = new PowerupBombDecorator(Game.Instance.AllBombs[_minerIndex]);
                    }
                    var myMessage = $"{nameof(CommandType.YOU_GOT_BONUS)} {json};";
                    var enemyMessage = $"{nameof(CommandType.ENEMY_GOT_BONUS)} {json};";
                    _userSocket.Send(Encoding.UTF8.GetBytes(myMessage));
                    _otherSocket.Send(Encoding.UTF8.GetBytes(enemyMessage));
                    allPrizes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}