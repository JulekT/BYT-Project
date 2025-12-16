namespace Library;

[Serializable]
public class Address
{
    private string _street;
    private string _city;
    private string _postalCode;
    private string _country;

    public string Street
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_street))
                throw new ValueNotAssigned("Street is not assigned");
            return _street;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Street cannot be empty");
            _street = value;
        }
    }

    public string City
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_city))
                throw new ValueNotAssigned("City is not assigned");
            return _city;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("City cannot be empty");
            _city = value;
        }
    }

    public string PostalCode
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_postalCode))
                throw new ValueNotAssigned("Postal code is not assigned");
            return _postalCode;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Postal code cannot be empty");
            _postalCode = value;
        }
    }

    public string Country
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_country))
                throw new ValueNotAssigned("Country is not assigned");
            return _country;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Country cannot be empty");
            _country = value;
        }
    }

    public Address(string street, string city, string postalCode, string country)
    {
        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {PostalCode}, {Country}";
    }
}
