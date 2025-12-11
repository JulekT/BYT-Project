using Library;
using NUnit.Framework;

namespace Tests
{
    public class StoreTests
    {
        [Test]
        public void Store_GetStockForProduct_Works()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            var stock = new Stock(store, apple, 10);

            var result = store.GetStockForProduct(apple);

            Assert.AreEqual(stock, result);
        }

        [Test]
        public void Store_RemoveStock_Works()
        {
            var store = new Store("Carrefour", "Street", "City", "00-000", "Poland");
            var apple = new Product("Apple", "BrandA", "A1", 5, 3);

            var stock = new Stock(store, apple, 10);

            store.RemoveStock(stock);

            Assert.IsFalse(store.Stock.Contains(stock));
        }
    }
}