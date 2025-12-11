using Library;
using NUnit.Framework;

namespace Tests
{
    public class ProductQuantityInOrderTests
    {
        [Test]
        public void PQO_CreatesCorrectly()
        {
            var p = new Product("Milk", "BrandA", "M2", 4, 2);
            var pq = new ProductQuantityInOrder(p, 5);

            Assert.AreEqual(p, pq.Product);
            Assert.AreEqual(5, pq.Quantity);
            Assert.AreEqual(20, pq.GetTotalPrice());
        }

        [Test]
        public void PQO_InvalidQuantity_Throws()
        {
            var p = new Product("Milk", "BrandA", "M2", 4, 2);

            Assert.Throws<ArgumentException>(() =>
            {
                new ProductQuantityInOrder(p, 0);
            });
        }
    }
}