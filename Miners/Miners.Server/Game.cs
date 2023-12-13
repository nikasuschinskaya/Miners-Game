using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Miners;
using Miners.Shared.Objects.Prizes.Base;
using OpenTK;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;

namespace Miners.Server
{
    public class Game
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureMine"].ToString();

        public static Game Instance { get; private set; }

        public static void SetGameSettings(IGameObject[,] level)
        {
            Instance = new Game(level);
        }

        private List<Miner> _allMiners;
        public List<IBomb> AllBombs { get; private set; }
        //private Miner _miner;
        private List<RectangleF> _notEmptyBlocksRectangles;
        //private RectangleF _minerRectangle;

        public IGameObject[,] Level { get; }
        public Miner GetMiner(int minerIndex) => _allMiners[minerIndex];
        public IBomb GetBomb(int minerIndex) => AllBombs[minerIndex];

        private Game(IGameObject[,] level)
        {
            Level = level;

            _allMiners = new List<Miner>();
            AllPrizes = new List<Prize>();
            AllBombs = new List<IBomb>()
            {
                new Bomb(Vector2.Zero, _texturePath),
                new Bomb(Vector2.Zero, _texturePath),
            };

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

                    var notEmptyBlockRectangle = new RectangleF(Level[x, y].Position.X * 48, Level[x, y].Position.Y * 48, 48, 48);
                    _notEmptyBlocksRectangles.Add(notEmptyBlockRectangle);
                }
            }

            //_miner = _allMiners[minerIndex];
        }

        public List<RectangleF> NotEmptyBlocksRectangles => _notEmptyBlocksRectangles;

        public List<Prize> AllPrizes { get; private set; }

        public bool SetNewMinerPosition(RectangleF newPosition, int minerIndex)
        {
            foreach (var emptyBlockRectangle in _notEmptyBlocksRectangles)
            {
                if (CheckCollision(newPosition, emptyBlockRectangle))
                {
                    return false;
                }
            }

            //_minerRectangle = newPosition;
            GetMiner(minerIndex).Position = new Vector2(newPosition.X / 48, newPosition.Y / 48);

            return true;
        }

        private bool CheckCollision(RectangleF firstGameObject, RectangleF secondGameObject) =>
            firstGameObject.IntersectsWith(secondGameObject);

        internal IEnumerable<Vector2> GetFreeCells()
        {
            for (var x = 0; x < Level.GetLength(0); x++)
            {
                for (var y = 0; y < Level.GetLength(1); y++)
                {
                    if (Level[x, y] is EmptyBlock empty)
                    {
                        yield return empty.Position;
                    }
                }
            }
        }
    }
}
