using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miners.Server.ObjectsFactories.BlocksFactories;
using Miners.Shared.Objects.Blocks;

namespace MinersTests.FactoriesTests
{
    [TestClass]
    public class EmptyBlockFactoryTests
    {
        private string _texturePath;
        private int _x;
        private int _y;

        [TestInitialize]
        public void Initialize()
        {
            _texturePath = null;
            _x = 0;
            _y = 0;
        }

        [TestMethod]
        public void CreateObject_ReturnsInstanceOfEmptyBlock()
        {
            // Arrange
            var factory = new EmptyBlockFactory(_x, _y);

            // Act
            var block = factory.CreateObject();

            // Assert
            Assert.IsInstanceOfType(block, typeof(EmptyBlock));
        }

        [TestMethod]
        public void CreateObject_SetsCorrectPositionAndTexturePath()
        {
            // Arrange
            var factory = new EmptyBlockFactory(_x, _y);

            // Act
            var result = factory.CreateObject();
            var block = (EmptyBlock)result;
            var position = block.Position;
            var texturePath = block.Path;

            // Assert
            Assert.AreEqual(_x, (int)position.X);
            Assert.AreEqual(_y, (int)position.Y);
            Assert.AreEqual(_texturePath, texturePath);
        }
    }
}
