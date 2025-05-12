public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly UserDbContext _userContext;

    public UserService(IUserRepository userRepository, UserDbContext context)
    {
        _userRepository = userRepository;
        _userContext = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllUsersAsync();

    public async Task<User?> GetUserByIdAsync(Guid id) => await _userRepository.GetUserByIdAsync(id);

    
    public async Task<User> CreateUser(User user)
    {
        
        await _userRepository.CreateUser(user);

        await _userContext.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null) return false;

        await _userRepository.DeleteUser(user);
        await _userContext.SaveChangesAsync();

        return true;

    }

}