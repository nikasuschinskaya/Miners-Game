using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miners.Server.ObjectsFactories.PrizesFactories;
using Miners.Shared.Objects.Prizes;
using System.Configuration;

namespace MinersTests.FactoriesTests
{
    [TestClass]
    public class PowerupFactoryTests
    {
        private string _texturePath;
        private int _x;
        private int _y;

        [TestInitialize]
        public void Initialize()
        {
            _texturePath = ConfigurationManager.AppSettings["texturePowerup"];
            _x = 0;
            _y = 0;
        }

        [TestMethod]
        public void CreateObject_ReturnsInstanceOfPowerupPrize()
        {
            // Arrange
            var factory = new PowerupFactory(_x, _y);

            // Act
            var prize = factory.CreateObject();

            // Assert
            Assert.IsInstanceOfType(prize, typeof(Powerup));
        }

        [TestMethod]
        public void CreateObject_SetsCorrectPositionAndTexturePath()
        {
            // Arrange
            var factory = new PowerupFactory(_x, _y);

            // Act
            var result = factory.CreateObject();
            var prize = (Powerup)result;
            var position = prize.Position;
            var texturePath = prize.Path;

            // Assert
            Assert.AreEqual(_x, (int)position.X);
            Assert.AreEqual(_y, (int)position.Y);
            Assert.AreEqual(_texturePath, texturePath);
        }
    }
}
