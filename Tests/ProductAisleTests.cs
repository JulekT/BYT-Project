using Library;
using NUnit.Framework;

namespace Tests
{
    public class ProductAisleTests
    {
        [Test]
        public void Product_SetAisle_WorksCorrectly()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var a1 = new Aisle(store, "Fruits");
            var a2 = new Aisle(store, "Veggies");

            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            apple.SetAisle(a1);
            Assert.AreEqual(a1, apple.Aisle);

            apple.SetAisle(a2);
            Assert.AreEqual(a2, apple.Aisle);
        }

        [Test]
        public void Product_SetAisle_Null_Throws()
        {
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            Assert.Throws<ArgumentNullException>(() =>
            {
                apple.SetAisle(null);
            });
        }
    }
}