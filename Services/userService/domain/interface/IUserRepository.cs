public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync();

    public Task<User?> GetUserByIdAsync(Guid id);

    public Task CreateUser(User user);

    public Task DeleteUser(User user);


}