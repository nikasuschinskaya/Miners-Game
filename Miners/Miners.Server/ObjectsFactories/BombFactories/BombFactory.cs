﻿using Miners.Server.ObjectsFactories.BlocksFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Bombs;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.BombFactories
{
    public class BombFactory : BlockFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureMine"].ToString();

        public BombFactory(int x, int y) : base(x, y)
        {
        }

        public override IGameObject CreateObject() =>
            new Bomb(new Vector2(X, Y), _texturePath);
    }
}
