namespace Contracts.Events
{
    public interface IProductCreated
    {
        Guid ProductId { get; }
        string ProductName { get; }
        string ProductCategory { get; }

        DateTime CreatedAt { get; }

        bool IsActive { get; }
    }
}