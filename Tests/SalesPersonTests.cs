using Library;
using NUnit.Framework;

namespace Tests
{
    public class SalesPersonTests
    {
        
        
        [Test]
        public void RegisterOrder_WorksCorrectly()
        {
            var sp = new SalesPerson("Mert", DateTime.Now, 3000, 0.2);
            var p1 = new Product("Apple", "BrandA", "A1", 5, 3);

            var items = new List<(Product product, int quantity)>
            {
                (p1, 3)
            };

            var order = sp.RegisterOrder(items);

            Assert.AreEqual(sp, order.SalesPerson);
            Assert.AreEqual(1, order.GetItems().Count);
            Assert.AreEqual(p1, order.GetItems()[0].Product);
            Assert.AreEqual(3, order.GetItems()[0].Quantity);
            
        }

        
        
        [Test]
        public void RegisterOrder_EmptyList_Throws()
        {
            var sp = new SalesPerson("Mert", DateTime.Now, 3000, 0.2);

            Assert.Throws<ArgumentException>(() =>
            {
                sp.RegisterOrder(new List<(Product, int)>());
            });
            
        }
        
    }
    
}






