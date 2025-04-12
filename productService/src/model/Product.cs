public class Product
{
    public Guid ProductId { get; private set; }

    public string ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int ProductQuantity { get; set; }

    public Product(string productName, string? productDescription)
    {
        ProductId = Guid.NewGuid();
        ProductName = productName;
        ProductDescription = productDescription;
        ProductQuantity = 0;
    }
}