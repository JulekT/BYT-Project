using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Library
{
    [Serializable]
    public class Supplier
    {

        private static List<Supplier> _extent = new();
        public static IReadOnlyCollection<Supplier> Extent => _extent.AsReadOnly();

        public static void SaveExtent(string fileName = "supplier_extent.json")
        {
            var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public static void LoadExtent(string fileName = "supplier_extent.json")
        {
            if (!File.Exists(fileName)) return;
            var json = File.ReadAllText(fileName);
            _extent = JsonSerializer.Deserialize<List<Supplier>>(json);
        }
        
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Supplier name cannot be empty.");
                _name = value;
            }
        }
        

        private Dictionary<string, Product> _productsByModel = new();

        public IReadOnlyDictionary<string, Product> ProductsByModel =>
            new Dictionary<string, Product>(_productsByModel);

  

        public Supplier(string name)
        {
            Name = name;
            _extent.Add(this);
        }

        public Supplier() { } 
        

        public void AddProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p), "Product cannot be null.");

            string qualifier = p.Model;

            if (string.IsNullOrWhiteSpace(qualifier))
                throw new ArgumentException("Product model cannot be empty for qualified association.");

            if (_productsByModel.ContainsKey(qualifier))
                throw new InvalidOperationException($"A product with model '{qualifier}' already exists for this supplier.");

            _productsByModel[qualifier] = p;

            if (p.Supplier != this)
                p.SetSupplier(this);
        }

        public Product GetProductByModel(string model)
        {
            if (_productsByModel.TryGetValue(model, out var product))
                return product;

            throw new KeyNotFoundException($"No product found for model '{model}'.");
        }

        public bool HasProduct(string model)
        {
            return _productsByModel.ContainsKey(model);
        }
        

        public void RemoveProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (!_productsByModel.ContainsKey(p.Model))
                throw new InvalidOperationException("This product is not registered under this supplier.");

            _productsByModel.Remove(p.Model);

            if (p.Supplier == this)
                p.RemoveSupplier();
        }
        

        public void Destroy()
        {
            foreach (var product in _productsByModel.Values.ToList())
                RemoveProduct(product);

            _extent.Remove(this);
        }
    }
}
