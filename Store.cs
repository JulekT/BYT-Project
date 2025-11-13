using System;
using System.Collections.Generic;

[Serializable]
public class Store
{
    private static List<Store> _extent = new List<Store>();

    private string _name;
    private string _street;
    private string _city;
    private string _postalCode;
    private string _country;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Store name cannot be empty");
            _name = value;
        }
    }

    public string Street
    {
        get => _street;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Street cannot be empty");
            _street = value;
        }
    }

    public string City
    {
        get => _city;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("City cannot be empty");
            _city = value;
        }
    }

    public string PostalCode
    {
        get => _postalCode;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Postal code cannot be empty");
            _postalCode = value;
        }
    }

    public string Country
    {
        get => _country;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Country cannot be empty");
            _country = value;
        }
    }

    public Store() { }

    public Store(string name, string street, string city, string postalCode, string country)
    {
        Name = name;
        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;

        AddStore(this);
    }

    private static void AddStore(Store s)
    {
        if (s == null) throw new ArgumentException("Store cannot be null");
        _extent.Add(s);
    }

    public static List<Store> GetExtent() => new List<Store>(_extent);

    public static void SetExtent(List<Store> list) => _extent = list ?? new List<Store>();
}
