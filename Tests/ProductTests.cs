using Library;
using System;
using System.IO;
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
            Product.Extent.Clear();

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
            Assert.That(ex.Message, Is.EqualTo("Product name is empty, you need to assign it first"));
        }

      

        [Test]
        public void ProductBrandAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Brand = "");
            Assert.That(ex.Message, Is.EqualTo("Brand cannot be empty"));
        }

        [Test]
        public void ProductBrandGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyProduct.Brand));
            Assert.That(ex.Message, Is.EqualTo("Product brand is empty, you need to assign it first"));
        }

    

        [Test]
        public void ProductModelAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Model = "");
            Assert.That(ex.Message, Is.EqualTo("Model cannot be empty"));
        }

        [Test]
        public void ProductModelGetEmptinessException()
        {
            var ex = Assert.Throws<ValueNotAssigned>(() => Console.WriteLine(emptyProduct.Model));
            Assert.That(ex.Message, Is.EqualTo("Product model is empty, you need to assign it first"));
        }

    

        [Test]
        public void ProductPriceAssignNegativeException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Price = -10);
            Assert.That(ex.Message, Is.EqualTo("Price must be positivea"));
        }

        [Test]
        public void ProductPriceGetNotAssignedException()
        {
            var ex = Assert.Throws<NumberIsNotPositive>(() => Console.WriteLine(emptyProduct.Price));
            Assert.That(ex.Message, Is.EqualTo("Price must be positive, you need to assign it first"));
        }

     

        [Test]
        public void ProductCostAssignNegativeException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Cost = -30);
            Assert.That(ex.Message, Is.EqualTo("Cost must be positive"));
        }

        [Test]
        public void ProductCostGetNotAssignedException()
        {
            var ex = Assert.Throws<NumberIsNotPositive>(() => Console.WriteLine(emptyProduct.Cost));
            Assert.That(ex.Message, Is.EqualTo("Product cost must be positive, you need to assign it first"));
        }

    
        [Test]
        public void ProductExtent_AddsCorrectly()
        {
            int oldCount = Product.Extent.Count;

            var p = new Product("Smartphone", "Samsung", "Galaxy S23", 4500, 3800);

            Assert.That(Product.Extent.Count, Is.EqualTo(oldCount + 1));
        }

        [Test]
        public void AddNullProductToExtentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => Product.AddProductToExtent(null));
            Assert.That(ex.Message, Is.EqualTo("Product cannot be null"));
        }


        [Test]
        public void SaveProductExtentFileCreation()
        {
            Product.SaveExtent();
            Assert.That(File.Exists("product_extent.json"), Is.True, "File wasn’t created properly");
        }

        [Test]
        public void LoadProductExtentFromFile()
        {
            var p = new Product("TV", "Sony", "Bravia", 3000, 2200);
            Product.AddProductToExtent(p);

            Product.SaveExtent();
            Product.Extent.Clear();

            Product.LoadExtent();

            Assert.That(Product.Extent.Count > 0, Is.True, "Loading didn’t work");
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
