using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoS.UI.DataModel;

namespace UnitTestProject
{
    [TestClass]
    public class InventoryManagerTests
    {
        [TestMethod]
        public void AddPart_IncreasesInventorySize()
        {
            // Arrange
            var manager = new InventoryManager();
            var initialCount = manager.Parts.Count;

            // Act
            manager.AddPartToDatabase("001", "Test Part", 10.00m, 5);

            // Assert
            Assert.AreEqual(initialCount + 1, manager.Parts.Count);
        }

        [TestMethod]
        public void UpdatePart_UpdatesPartCorrectly()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddPartToDatabase("001", "Test Part", 10.00m, 5);
            // Act
            manager.UpdatePartInformation("001", "Updated Part", 15.00m, 10);
            var part = manager.Parts.Find(p => p.PartNumber == "001");
            // Assert
            Assert.AreEqual("Updated Part", part.Description);
            Assert.AreEqual(15.00m, part.Price);
            Assert.AreEqual(10, part.Quantity);
        }

        [TestMethod]
        public void RemovePart_DecreasesInventorySize()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddPartToDatabase("001", "Test Part", 10.00m, 5);
            var initialCount = manager.Parts.Count;

            // Act
            manager.RemovePartFromDatabase("001");

            // Assert
            Assert.AreEqual(initialCount - 1, manager.Parts.Count);
        }

        [TestMethod]
        public void AddPartQuantity_UpdatesQuantityCorrectly()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddPartToDatabase("001", "Test Part", 10.00m, 5);

            // Act
            manager.AddPartQuantity("001", 3);
            var part = manager.Parts.Find(p => p.PartNumber == "001");

            // Assert
            Assert.AreEqual(8, part.Quantity);
        }

        [TestMethod]
        public void Print_ReturnsCorrectFormat()
        {
            // Arrange
            var manager = new InventoryManager();
            manager.AddPartToDatabase("001", "Test Part", 10.00m, 5);

            // Act
            var result = manager.Print();

            // Assert
            StringAssert.Contains(result, "Part Number: 001");
            StringAssert.Contains(result, "Description: Test Part");
            StringAssert.Contains(result, "Price: 10");
            StringAssert.Contains(result, "Quantity: 5");
        }
    }

}
