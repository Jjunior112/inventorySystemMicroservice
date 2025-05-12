using Microsoft.EntityFrameworkCore;

public class StockDbContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }
    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

}