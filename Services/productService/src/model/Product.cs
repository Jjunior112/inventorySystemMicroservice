public class Product
{
    public Guid ProductId { get; private set; }

    public string ProductName { get; set; }

    public string ProductCategory { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public Product(string productName, string productCategory)
    {
        ProductId = Guid.NewGuid();
        ProductName = productName;
        ProductCategory = productCategory;
        CreatedAt = DateTime.Now;

    }

}