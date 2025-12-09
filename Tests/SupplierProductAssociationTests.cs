using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class SupplierProductAssociationTests
    {
        [SetUp]
        public void Reset()
        {
            Supplier.Extent.Clear();
            Product.Extent.Clear();
        }

        [Test]
        public void QualifiedAssociation_FindProductByModel()
        {
            var supplier = new Supplier("Xiaomi Supplier");
            var product = new Product("Xiaomi 12", "Xiaomi", "12", 2000, 1200);
            supplier.AddProduct(product);
            var result = supplier.GetProductByModel("12");
            Assert.That(result, Is.EqualTo(product));
        }

        [Test]
        public void AddProductSetsBackReference()
        {
            var supplier = new Supplier("Sony Supplier");
            var product = new Product("Headphones", "Sony", "XM5", 1500, 900);
            supplier.AddProduct(product);
            Assert.That(product.Supplier, Is.EqualTo(supplier));
        }
    }
}
