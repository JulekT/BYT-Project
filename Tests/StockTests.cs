using Library;
using NUnit.Framework;

namespace Tests
{
    public class StockTests
    {
        [SetUp]
        public void Setup()
        {
            // Clear static extents to avoid pollution
            typeof(Stock).GetField("_extent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .SetValue(null, new List<Stock>());
        }

        [Test]
        public void Stock_Creation_WorksCorrectly()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var product = new Product("Apple", "BrandA", "A1", 5, 3);

            var stock = new Stock(store, product, 20);

            Assert.AreEqual(20, stock.Quantity);
            Assert.AreEqual(store, stock.Store);
            Assert.AreEqual(product, stock.Product);
            Assert.Contains(stock, store.Stock.ToList());
        }

        [Test]
        public void Stock_DuplicateProductInStore_Throws()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            var s1 = new Stock(store, apple, 20);

            Assert.Throws<InvalidOperationException>(() =>
            {
                new Stock(store, apple, 10);
            });
        }

        [Test]
        public void Stock_NonPositiveQuantity_Throws()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            Assert.Throws<ArgumentException>(() =>
            {
                new Stock(store, apple, 0);
            });
        }

        [Test]
        public void Stock_PlaceInAisle_DecreasesQuantity()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var aisle = new Aisle(store, "Fruits");
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            var stock = new Stock(store, apple, 5);

            stock.PlaceInAisle(aisle);

            Assert.AreEqual(4, stock.Quantity);
            Assert.AreEqual(aisle, apple.Aisle);
        }

        [Test]
        public void Stock_PlaceInAisle_EmptyStock_Throws()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var aisle = new Aisle(store, "Fruits");
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            var stock = new Stock(store, apple, 1);

            stock.PlaceInAisle(aisle);

            Assert.Throws<InvalidOperationException>(() =>
            {
                stock.PlaceInAisle(aisle);
            });
        }
    }
}
