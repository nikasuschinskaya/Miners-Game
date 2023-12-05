using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miners.Shared.Objects.Bombs;
using Miners.Shared.Objects.Prizes;

namespace MinersTests.DecoratorsTests
{
    [TestClass]
    public class LetupBombDecoratorTests
    {
        private const int Amount = 1;

        [TestMethod]
        public void Radius_Get_ReturnsDecoratedBombRadius()
        {
            // Arrange
            int expectedRadius = 3;
            IBomb bomb = new Bomb { Radius = expectedRadius };
            LetupBombDecorator decorator = new LetupBombDecorator(bomb);

            // Act
            int actualRadius = decorator.Radius;

            // Assert
            Assert.AreEqual(expectedRadius, actualRadius);
        }

        [TestMethod]
        public void Radius_Set_SetsDecoratedBombRadius()
        {
            // Arrange
            int expectedRadius = 2;
            IBomb bomb = new Bomb();
            LetupBombDecorator decorator = new LetupBombDecorator(bomb);

            // Act
            decorator.Radius = expectedRadius;
            int actualRadius = decorator.Radius;

            // Assert
            Assert.AreEqual(expectedRadius, actualRadius);
        }

        [TestMethod]
        public void Damage_Get_ReturnsDecoratedBombDamage()
        {
            // Arrange
            int expectedDamage = 2;
            IBomb bomb = new Bomb { Damage = 3 };
            LetupBombDecorator decorator = new LetupBombDecorator(bomb);

            // Act
            int actualDamage = decorator.Damage;

            // Assert
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [TestMethod]
        public void Damage_Set_SetsDecoratedBombDamage()
        {
            // Arrange
            int expectedDamage = 2;
            IBomb bomb = new Bomb();
            LetupBombDecorator decorator = new LetupBombDecorator(bomb);

            // Act
            decorator.Damage = expectedDamage + Amount;
            int actualDamage = decorator.Damage;

            // Assert
            Assert.AreEqual(expectedDamage, actualDamage);
        }
    }

}
