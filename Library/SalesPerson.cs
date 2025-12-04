using System;
namespace BYT_Project
{
    [Serializable]
    public class SalesPerson : Staff
    {
        private double _commissionRate;


        private List<Order> _orders = new();


        public double CommissionRate
        {
            get => _commissionRate;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentException("Commission rate must be between 0 and 1.");
                _commissionRate = value;
            }
        }


        public SalesPerson(string name, DateTime employmentDate, double baseSalary, double commissionRate)
            : base(name, employmentDate, baseSalary)
        {
            CommissionRate = commissionRate;


            Staff.AddStaffTToExtent(this);
        }






        public Order RegisterOrder(List<(Product product, int quantity)> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Order must contain at least one item.");

            Order order = new Order(this);

            foreach (var item in items)
                order.AddProduct(item.product, item.quantity);

            _orders.Add(order);
            return order;
        }


        public IReadOnlyList<Order> GetRegisteredOrders()
        {
            return _orders.AsReadOnly();
        }
    }
}
