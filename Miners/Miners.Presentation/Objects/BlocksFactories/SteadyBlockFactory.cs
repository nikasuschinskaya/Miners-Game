﻿using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System.Configuration;

namespace Miners.Presentation.Objects.BlocksFactories
{
    public class SteadyBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureSteadyBlockPath"].ToString();

        private int _x;
        private int _y;

        public SteadyBlockFactory(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override IBlock GetBlock()
        {
            return new SteadyBlock(new Vector2(_x, _y), TextureProcessing.LoadTexture(_texturePath));
        }
    }
}