public class Stock
{
    public Guid StockId { get; private set; }
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductCategory { get; set; }


    public DateTime CreatedAt { get; set; }

    public int ProductQuantity { get; set; }

    public bool IsActive { get; set; }



    public Stock(Guid productId, string productName, string productCategory, DateTime createdAt, bool isActive)
    {
        StockId = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        ProductCategory = productCategory;
        CreatedAt = createdAt;
        ProductQuantity = 0;
        IsActive = isActive;
    }

}