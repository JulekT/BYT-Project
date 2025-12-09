using Library;
using System;
using NUnit.Framework;

namespace Tests
{
    public class AisleTests
    {
        private Aisle emptyAisle;
        private Aisle namedAisle;

        [SetUp]
        public void Setup()
        {
            emptyAisle = new Aisle();
            namedAisle = new Aisle("Laptops");
        }
    
        [Test]
        public void AisleNameAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyAisle.Name = "");
            Assert.That(ex.Message, Is.EqualTo("Aisle name cannot be empty"));
        }

        [Test]
        public void AisleNameGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyAisle.Name));
            Assert.That(ex.Message, Is.EqualTo("Aisle name is empty, you need to assign it firstly"));
        }

   
        [Test]
        public void AddProduct_Null_ThrowsException()
        {
            Product p = null;
            var ex = Assert.Throws<ArgumentException>(() => namedAisle.AddProduct(p));
            Assert.That(ex.Message, Is.EqualTo("Product cannot be null."));
        }

        [Test]
        public void AddProduct_AddsCorrectly()
        {
            var p = new Product("Gaming Mouse", "Logitech", "G502 Hero", 250, 150);
            namedAisle.AddProduct(p);
            Assert.That(namedAisle.Products.Count, Is.EqualTo(1));
            Assert.That(p.Aisle, Is.EqualTo(namedAisle));
        }

   
        [Test]
        public void RemoveProduct_RemovesCorrectly()
        {
            var p = new Product("Monitor", "LG", "UltraWide 34\"", 1600, 1300);
            namedAisle.AddProduct(p);
            namedAisle.RemoveProduct(p);
            Assert.That(namedAisle.Products.Count, Is.EqualTo(0));
            Assert.That(p.Aisle, Is.Null);
        }

        [Test]
        public void RemoveProduct_WhenProductNotInAisle_DoesNothing()
        {
            var p = new Product("Keyboard", "Keychron", "K6", 400, 250);
            Assert.DoesNotThrow(() => namedAisle.RemoveProduct(p));
            Assert.That(namedAisle.Products.Count, Is.EqualTo(0));
            Assert.That(p.Aisle, Is.Null);
        }
    }
}
