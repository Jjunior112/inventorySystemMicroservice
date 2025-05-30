using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class UserDbContext(DbContextOptions options) : IdentityDbContext<User>(options);
