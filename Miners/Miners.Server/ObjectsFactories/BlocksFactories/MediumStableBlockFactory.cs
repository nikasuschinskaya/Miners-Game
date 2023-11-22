﻿using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Blocks;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.BlocksFactories
{
    public class MediumStableBlockFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureMediumStableBlockPath"].ToString();

        public MediumStableBlockFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject()
        {
            return new MediumStableBlock(new Vector2(X, Y), _texturePath);
        }
    }
}