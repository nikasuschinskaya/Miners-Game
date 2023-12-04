using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miners.Server.ObjectsFactories.BombFactories;
using Miners.Shared.Objects.Bombs;
using System.Configuration;

namespace MinersTests.FactoriesTests
{
    [TestClass]
    public class BombFactoryTests
    {
        private string _texturePath;
        private int _x;
        private int _y;

        [TestInitialize]
        public void Initialize()
        {
            _texturePath = ConfigurationManager.AppSettings["textureMine"];
            _x = 0;
            _y = 0;
        }

        [TestMethod]
        public void CreateObject_ReturnsInstanceOfBomb()
        {
            // Arrange
            var factory = new BombFactory(_x, _y);

            // Act
            var bomb = factory.CreateObject();

            // Assert
            Assert.IsInstanceOfType(bomb, typeof(Bomb));
        }

        [TestMethod]
        public void CreateObject_SetsCorrectPositionAndTexturePath()
        {
            // Arrange
            var factory = new BombFactory(_x, _y);

            // Act
            var result = factory.CreateObject();
            var bomb = (Bomb)result;
            var position = bomb.Position;
            var texturePath = bomb.Path;

            // Assert
            Assert.AreEqual(_x, (int)position.X);
            Assert.AreEqual(_y, (int)position.Y);
            Assert.AreEqual(_texturePath, texturePath);
        }
    }
}
