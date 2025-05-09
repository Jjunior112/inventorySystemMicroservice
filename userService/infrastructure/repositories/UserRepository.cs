using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{

    private readonly UserDbContext _userContext;
    
    public UserRepository(UserDbContext context)
    {
        _userContext = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userContext.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetUserByIdAsync(Guid id) => await _userContext.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();

    public Task CreateUser(User user)
    {
        _userContext.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteUser(User user)
    {
        _userContext.Remove(user);

        return Task.CompletedTask;
    }
}