using Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class SupplierTests
    {
        [SetUp]
        public void Setup()
        {
            typeof(Supplier)
                .GetField("_extent", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, new List<Supplier>());

            typeof(Product)
                .GetField("_extent", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, new List<Product>());

            typeof(Supplier)
                .GetField("_suppliersByCompany", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, new Dictionary<string, Supplier>());
        }

        [Test]
        public void AddProduct_WorksCorrectly()
        {
            var supplier = new Supplier("ABC Supplier", "ABC");
            var p = new Product("Phone", "BrandX", "M1", 500, 300);

            supplier.AddProduct(p);

            Assert.IsTrue(supplier.HasProduct("M1"));
        }

        [Test]
        public void AddProduct_DuplicateModel_Throws()
        {
            var supplier = new Supplier("ABC Supplier", "ABC");
            var p1 = new Product("Phone", "BrandX", "M1", 500, 300);
            var p2 = new Product("Tablet", "BrandX", "M1", 800, 500);

            supplier.AddProduct(p1);

            Assert.Throws<InvalidOperationException>(() => supplier.AddProduct(p2));
        }

        [Test]
        public void RemoveProduct_WorksCorrectly()
        {
            var supplier = new Supplier("ABC Supplier", "ABC");
            var p = new Product("Phone", "BrandX", "M1", 500, 300);

            supplier.AddProduct(p);
            supplier.RemoveProduct(p);

            Assert.IsFalse(supplier.HasProduct("M1"));
        }
    }
}