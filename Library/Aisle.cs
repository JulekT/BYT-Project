using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace Library
{
    [Serializable]
    public class Aisle
    {

        private static List<Aisle> _extent = new();
        public static IReadOnlyCollection<Aisle> Extent => _extent.AsReadOnly();

        public static void SaveExtent(string fileName = "aisle_extent.json")
        {
            var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public static void LoadExtent(string fileName = "aisle_extent.json")
        {
            if (!File.Exists(fileName)) return;
            var json = File.ReadAllText(fileName);
            _extent = JsonSerializer.Deserialize<List<Aisle>>(json);
        }
        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Aisle name cannot be empty");
                _name = value;
            }
        }



        public Store Store { get; private set; }

        public void SetStore(Store store)
        {
            Store = store;
        }

        
        public Aisle(Store store, string name)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store), "Aisle cannot exist without a Store");

            Name = name;

            store.AddAisle(this);

            _extent.Add(this);
        }

        [Obsolete("Parameterless constructor for serialization only", true)]
        public Aisle() { }
        

        public void Destroy()
        {
            foreach (var p in _products.ToList())
                RemoveProduct(p);

            Store = null;

            _extent.Remove(this);
        }
        

        private HashSet<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.ToList().AsReadOnly();

        public void AddProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (_products.Contains(p))
                return;

            _products.Add(p);

            if (p.Aisle != this)
                p.SetAisle(this);
        }

        public void RemoveProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (!_products.Contains(p))
                throw new InvalidOperationException("Product does not belong to this aisle.");
            if(_products.Count  == 1)
                throw new InvalidOperationException("A aisle must have at least one product (1..*). Cannot remove the last one.");

            _products.Remove(p);

            if (p.Aisle == this)
                p.RemoveAisle();
        }
    }
}
