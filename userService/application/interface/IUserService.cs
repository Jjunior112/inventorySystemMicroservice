public interface IUserService
{
    public Task<IEnumerable<User>> GetAllUsersAsync();

    public Task<User?> GetUserByIdAsync(Guid id);

    public Task<User> CreateUser(User user);

    public Task<bool> DeleteUser(Guid id);


}