using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miners.Server.ObjectsFactories.MinersFactories;
using Miners.Shared.Objects.Miners;
using System.Configuration;

namespace MinersTests.FactoriesTests
{
    [TestClass]
    public class MinerFactoryTests
    {
        private string _texturePath;
        private int _x;
        private int _y;
        private float _speed;

        [TestInitialize]
        public void Initialize()
        {
            _texturePath = ConfigurationManager.AppSettings["textureFirstPlayer"];
            _x = 0;
            _y = 0;
            _speed = 20;
        }

        [TestMethod]
        public void CreateObject_ReturnsInstanceOfMiner()
        {
            // Arrange
            var factory = new MinerFactory(_x, _y);

            // Act
            var result = factory.CreateObject();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Miner));
        }

        [TestMethod]
        public void CreateObject_SetsCorrectPositionTexturePathAndSpeed()
        {
            // Arrange
            var factory = new MinerFactory(_x, _y);

            // Act
            var result = factory.CreateObject();
            var miner = (Miner)result;
            var position = miner.Position;
            var texturePath = miner.Path;
            var speed = miner.Speed;

            // Assert
            Assert.AreEqual(_x, (int)position.X);
            Assert.AreEqual(_y, (int)position.Y);
            Assert.AreEqual(_texturePath, texturePath);
            Assert.AreEqual(_speed, speed);
        }
    }
}
