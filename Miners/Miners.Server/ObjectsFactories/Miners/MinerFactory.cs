using Miners.Server.ObjectsFactories.Base;
using Miners.Shared.Objects.Base;
using Miners.Shared.Objects.Miners;
using OpenTK;
using System.Configuration;

namespace Miners.Server.ObjectsFactories.Miners
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
            return new Miner(new Vector2(_x, _y), _texturePath, _speed);
        }


    }
}
