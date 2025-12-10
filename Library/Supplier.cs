using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    [Serializable]
    public class Supplier
    {


        private static List<Supplier> _extent = new();
        public static IReadOnlyCollection<Supplier> Extent => _extent.AsReadOnly();


        
        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Company name cannot be empty");

                _companyName = value;
            }
        }

        private string _contactName;
        public string ContactName
        {
            get => _contactName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Contact name cannot be empty");

                _contactName = value;
            }
        }
        

        public Store Store { get; private set; }

        internal void SetStore(Store store)
        {
            Store = store; 
        }
        

        private HashSet<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.ToList();

        public void AddProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (_products.Contains(p))
                return;

            _products.Add(p);
        }

        public void RemoveProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (_products.Contains(p))
                _products.Remove(p);
        }
        

        public Supplier(string companyName, string contactName)
        {
            CompanyName = companyName;
            ContactName = contactName;

            _extent.Add(this);
        }

        public Supplier() { }
    }
}
