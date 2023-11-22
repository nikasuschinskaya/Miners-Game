using Miners.Server.ObjectsFactories.BlocksFactories;
using Miners.Server.ObjectsFactories.Miners;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Miners.Server.Level
{
    public class LevelLoader
    {
        private readonly string _levelPath = ConfigurationManager.AppSettings["levelPath"].ToString();

        private readonly Dictionary<char, Func<int, int, IGameObject>> _blockFactories = new Dictionary<char, Func<int, int, IGameObject>>
        {
            { '#', (x, y) => new SteadyBlockFactory(x, y).CreateObject() },
            { '1', (x, y) => new MediumStableBlockFactory(x, y).CreateObject() },
            { '2', (x, y) => new WeakResistantBlockFactory(x, y).CreateObject() },
            { '.', (x, y) => new EmptyBlockFactory(x, y).CreateObject() },
            { '*', (x, y) => new MinerFactory(x, y).CreateObject() }
        };

        public IGameObject[,] LoadLevel()
        {
            var random = new Random();
            var lines = File.ReadAllLines(_levelPath + $"{random.Next(1, 3)}.txt");
            var width = int.Parse(lines[0].Split(' ')[0]);
            var height = int.Parse(lines[0].Split(' ')[1]);

            var level = new IGameObject[width, height];

            for (int y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (int x = 0; x < width; x++)
                {
                    var symbol = line[x];
                    if (_blockFactories.TryGetValue(symbol, out var blockFactory))
                    {
                        level[x, y] = blockFactory(x, y);
                    }
                    else
                    {
                        level[x, y] = new EmptyBlock(new Vector2(x, y), null);
                    }
                }
            }

            return level;
        }
    }
}
