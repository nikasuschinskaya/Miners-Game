using Miners.Presentation.Enums;
using Miners.Presentation.Objects.Base;
using Miners.Presentation.Objects.Blocks;
using Miners.Presentation.Objects.Blocks.Base;
using Miners.Presentation.Objects.BlocksFactories.Base;
using Miners.Presentation.Render;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miners.Presentation.Objects.Miners
{
    public class MinerFactory : GameObjectFactory
    {
        private readonly string _texturePath = ConfigurationManager.AppSettings["textureFirstPlayer"].ToString();

        //private Vector2 _position;

        

        //private Transform _transform;

        //public Transform Transform => _transform;

        private int _x;
        private int _y;
        private float _speed;

        public MinerFactory(int x, int y)
        {
            _x = x;
            _y = y;
            _speed = 20;
            //_position = new Vector2(_x, _y);
        }

        public override IGameObject CreateObject()
        {
            return new Miner(new Vector2(_x, _y), TextureProcessing.LoadTexture(_texturePath), _speed);
        }


    }
}
