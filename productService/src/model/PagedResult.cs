public class PagedResult
{
    public List<Product> Products { get; set; }
    public int TotalCounts { get; set; }
    public int Page { get; set; }

    public int PageSize { get; set; }
}