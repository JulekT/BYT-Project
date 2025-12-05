using Library;
using System;
using NUnit.Framework;

namespace Tests
{
    public class ProductTests
    {
        private Product emptyProduct;
        private Product fullProduct;

        [SetUp]
        public void Setup()
        {
            emptyProduct = new Product();
            fullProduct = new Product("Laptop", "Lenovo", "IdeaPad 5", 3200, 2500);
        }

        
        [Test]
        public void ProductNameAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Name = "");
            Assert.That(ex.Message, Is.EqualTo("Product name cannot be empty"));
        }

    
        [Test] 
        public void ProductNameGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyProduct.Name));
            Assert.That(ex.Message, Is.EqualTo("Product name is empty, you need to assign it "));
        }

    
        [Test]
        public void ProductPriceAssignNegativeException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Price = -10);
            Assert.That(ex.Message, Is.EqualTo("Price must be positivea"));
        }

        
        [Test]
        public void ProductExtent_AddsCorrectly()
        {
            int oldCount = Product.Extent.Count;

            var p = new Product("Smartphone", "Samsung", "Galaxy S23", 4500, 3800);

            Assert.That(Product.Extent.Count, Is.EqualTo(oldCount + 1));
        }

        [Test]
        public void ProductAisleReverseConnectionWorks()
        {
            var aisle = new Aisle("Laptops");

            var product = new Product("Gaming Laptop", "ASUS", "ROG Strix G17", 7500, 6000);

            aisle.AddProduct(product);

            Assert.That(product.Aisle, Is.EqualTo(aisle));
            Assert.That(aisle.Products.Count, Is.EqualTo(1));
        }
    }
}
