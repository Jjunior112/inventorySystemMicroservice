using Microsoft.EntityFrameworkCore;

public class ProductDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }


}