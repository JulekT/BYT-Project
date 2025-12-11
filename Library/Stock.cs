namespace Library
{
    [Serializable]
    public class Stock
    {
        private static List<Stock> _extent = new();
        public static IReadOnlyList<Stock> Extent => _extent.AsReadOnly();

        public Store Store { get; private set; }
        public Product Product { get; private set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Stock quantity cannot be negative.");
                _quantity = value;
            }
        }

        public Stock(Store store, Product product, int quantity)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");

            if (store.GetStockForProduct(product) != null)
                throw new InvalidOperationException("This store already has stock for this product.");

            Store = store;
            Product = product;
            Quantity = quantity;

            store.AddStock(this);
            _extent.Add(this);
        }

       
        
        public void PlaceInAisle(Aisle aisle)
        {
            if (aisle == null)
                throw new ArgumentNullException(nameof(aisle));

            if (Quantity <= 0)
                throw new InvalidOperationException("No stock remaining to place product on an aisle.");

            Quantity -= 1;

            Product.SetAisle(aisle);
        }

        public void Destroy()
        {
            Store.RemoveStock(this);
            _extent.Remove(this);
        }
    }
}
