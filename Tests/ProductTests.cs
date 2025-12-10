using Library;
using NUnit.Framework;
using System.IO;

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
        public void ProductBrandAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Brand = "");
            Assert.That(ex.Message, Is.EqualTo("Brand cannot be empty"));
        }

        [Test]
        public void ProductModelAssignEmptinessException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Model = "");
            Assert.That(ex.Message, Is.EqualTo("Model cannot be empty"));
        }

        [Test]
        public void ProductPriceAssignNegativeException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Price = -10);
            Assert.That(ex.Message, Is.EqualTo("Price must be positive"));
        }

        [Test]
        public void ProductCostAssignNegativeException()
        {
            var ex = Assert.Throws<ArgumentException>(() => emptyProduct.Cost = -30);
            Assert.That(ex.Message, Is.EqualTo("Cost must be positive"));
        }

        [Test]
        public void ProductExtent_AddsCorrectly()
        {
            int old = Product.Extent.Count;

            var p = new Product("TV", "Sony", "Bravia", 3000, 2500);

            Assert.That(Product.Extent.Count, Is.EqualTo(old + 1));
        }

        [Test]
        public void SaveExtent_CreatesFile()
        {
            Product.SaveExtent("product_test.json");

            Assert.That(File.Exists("product_test.json"), Is.True);

            File.Delete("product_test.json");
        }

        [Test]
        public void LoadExtent_LoadsProducts()
        {
            Product.SaveExtent("product_test.json");

            Product.LoadExtent("product_test.json");

            Assert.That(Product.Extent.Count > 0, Is.True);

            File.Delete("product_test.json");
        }

        [Test]
        public void ProductAisleReverseConnectionWorks()
        {
            var store = new Store("MegaStore", "Street", "City", "00000", "Country");
            var aisle = new Aisle(store, "Laptops");
            var product = new Product("Gaming Laptop", "ASUS", "ROG Strix", 7000, 6000);

            aisle.AddProduct(product);

            Assert.That(product.Aisle, Is.EqualTo(aisle));
            Assert.That(aisle.Products.Count, Is.EqualTo(1));
        }
    }
}
