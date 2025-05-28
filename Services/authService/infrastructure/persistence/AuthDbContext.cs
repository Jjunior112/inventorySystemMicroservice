using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AuthDbContext : DbContext
{
    
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {

    }
}