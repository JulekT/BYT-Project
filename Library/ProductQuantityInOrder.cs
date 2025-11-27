using System;

[Serializable]
public class ProductQuantityInOrder
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    public ProductQuantityInOrder(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null.");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        Product = product;
        Quantity = quantity;
    }

    public double GetTotalPrice()
    {
        return Product.Price * Quantity;
    }
}
