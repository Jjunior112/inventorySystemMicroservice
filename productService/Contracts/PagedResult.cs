public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCounts { get; set; }
    public int Page { get; set; }

    public int PageSize { get; set; }
}