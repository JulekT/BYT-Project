using Library;
using System;
using System.IO;
using NUnit.Framework;

namespace Tests
{
    public class SupplierTests
    {
        private readonly Supplier emptySupplier = new Supplier("Test");
        private readonly Supplier filledSupplier = new Supplier("Apple Supplier");

        [SetUp]
        public void Clear()
        {
            Supplier.Extent.Clear();
            Product.Extent.Clear();
        }

        [Test]
        public void AddDuplicateProductModelException()
        {
            var p1 = new Product("iPhone", "Apple", "15", 5000, 3500);
            var p2 = new Product("iPhone Pro", "Apple", "15", 6500, 4500);

            filledSupplier.AddProduct(p1);

            var ex = Assert.Throws<InvalidOperationException>(() => filledSupplier.AddProduct(p2));
            Assert.That(ex.Message, Is.EqualTo("Product with model 15 already exists for this supplier"));
        }

        [Test]
        public void GetProductByModel_ModelNotFoundException()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => filledSupplier.GetProductByModel("NOT_EXIST"));
            Assert.That(ex.Message, Is.EqualTo("No product found with model NOT_EXIST"));
        }

        [Test]
        public void AddProductAssignsSupplierCorrectly()
        {
            var p = new Product("MacBook", "Apple", "M2", 9000, 6000);
            filledSupplier.AddProduct(p);

            Assert.That(p.Supplier, Is.EqualTo(filledSupplier));
        }
    }
}
