﻿using Miners.Server.ObjectsFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Miners;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.MinersFactories
{
    public class MinerFactory : GameObjectFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureFirstPlayer"].ToString();

        private readonly int _x;
        private readonly int _y;
        private readonly float _speed;

        /// <summary>Initializes a new instance of the <see cref="MinerFactory" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public MinerFactory(int x, int y)
        {
            _x = x;
            _y = y;
            _speed = 20;
        }
        /// <inheritdoc />
        public override IGameObject CreateObject() => 
            new Miner(new Vector2(_x, _y), _texturePath, _speed);

    }
}
