using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly UserDbContext _context;

    public UserService(UserDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers() => await _context.Users.ToListAsync();

    public async Task<User?> GetUserById(Guid id) => await _context.Users.FindAsync(id);

    public async Task RegisterUser(User user)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        user.Password = hashedPassword;

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task<string?> LoginUser(LoginRequest request, IConfiguration config)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == request.userAccess || u.UserName == request.userAccess);

        if (user == null) return null;

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.password, user.Password);

        if (!isValidPassword) return null;

        var token = JwtHelper.GenerateToken(user, config);

        return token;

    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return false;

        _context.Remove(user);

        await _context.SaveChangesAsync();

        return true;

    }
}