using Miners.Presentation.Objects;
using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Miners.Presentation.Level
{
    public class LevelLoader
    {
        private readonly string _levelPath = ConfigurationManager.AppSettings["levelPath"].ToString();
        private readonly Dictionary<char, Func<int, int, IBlock>> _blockFactories = new Dictionary<char, Func<int, int, IBlock>>
        {
            { '#', (x, y) => new SteadyBlockFactory(x, y).GetBlock() },
            { '1', (x, y) => new MediumStableBlockFactory(x, y).GetBlock() },
            { '2', (x, y) => new WeakResistantBlockFactory(x, y).GetBlock() },
            { '.', (x, y) => new EmptyBlockFactory(x, y).GetBlock() },
            { '*', (x, y) => new MinerFactory(x, y).GetBlock() }
        };

        public IBlock[,] LoadLevel()
        {
            var random = new Random();
            var lines = File.ReadAllLines(_levelPath + $"{random.Next(1, 3)}.txt");
            var width = int.Parse(lines[0].Split(' ')[0]);
            var height = int.Parse(lines[0].Split(' ')[1]);

            var level = new IBlock[width, height];

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