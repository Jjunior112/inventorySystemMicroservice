using Microsoft.EntityFrameworkCore;

public class OperationDbContext : DbContext
{
    public DbSet<Operation> Operations { get; set; }

    public OperationDbContext(DbContextOptions<OperationDbContext> options) : base(options) { }
}
