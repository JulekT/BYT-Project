using Library;
using NUnit.Framework;

namespace Tests
{
    public class CashierEmploymentTests
    {
        [Test]
        public void Cashier_CannotHaveNullEmploymentType()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Cashier("Mert", DateTime.Now, 3000, null);
            });
        }

        [Test]
        public void Cashier_CanChangeEmploymentType()
        {
            var ft = new FullTimeEmployment();
            var pt = new PartTimeEmployment();

            var cashier = new Cashier("Mert", DateTime.Now, 3000, ft);

            cashier.ChangeEmploymentType(pt);

            Assert.AreEqual(pt, cashier.EmploymentType);
        }
    }
}