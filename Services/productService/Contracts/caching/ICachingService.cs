public interface ICachingService
{
    Task SetAsync(string key, string value);

    Task<string> GetAsync(string key);

    Task DeleteAsync(string key);


}