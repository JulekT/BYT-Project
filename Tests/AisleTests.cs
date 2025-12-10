using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class AisleTests
    {
        private Aisle emptyAisle;
        private Aisle namedAisle;
        private Store testStore;

        [SetUp]
        public void Setup()
        {
            testStore = new Store("TestStore", "Street", "City", "00-000", "Country");

            emptyAisle = new Aisle(testStore, "EmptyAisle");
            namedAisle = new Aisle(testStore, "Laptops");
        }

        [Test]
        public void AisleNameAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyAisle.Name = "");
            Assert.That(ex.Message, Is.EqualTo("Aisle name cannot be empty"));
        }

        [Test]
        public void AddProduct_Null_ThrowsException()
        {
            Product p = null;

            var ex = Assert.Throws<ArgumentNullException>(() => namedAisle.AddProduct(p));
            Assert.That(ex.ParamName, Is.EqualTo("p"));
        }

        [Test]
        public void AddProduct_AddsCorrectly()
        {
            var p = new Product("Gaming Mouse", "Logitech", "G502", 250, 150);

            namedAisle.AddProduct(p);

            Assert.That(namedAisle.Products.Count, Is.EqualTo(1));
            Assert.That(p.Aisle, Is.EqualTo(namedAisle));
        }

        [Test]
        public void RemoveProduct_RemovesCorrectly()
        {
            var p = new Product("Monitor", "LG", "UltraWide", 1600, 1300);

            namedAisle.AddProduct(p);
            namedAisle.RemoveProduct(p);

            Assert.That(namedAisle.Products.Count, Is.EqualTo(0));
            Assert.That(p.Aisle, Is.Null);
        }

        [Test]
        public void RemoveProduct_WhenProductNotInAisle_Throws()
        {
            var p = new Product("Keyboard", "Keychron", "K6", 400, 250);

            var ex = Assert.Throws<InvalidOperationException>(() => namedAisle.RemoveProduct(p));
            Assert.That(ex.Message, Is.EqualTo("Product not in aisle"));
        }
    }
}
