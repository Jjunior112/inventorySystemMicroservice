namespace Contracts.Events
{
    public interface IProductCreated
    {
        Guid ProductId { get; }
        string ProductName { get; }
        string ProductDescription { get; }
        int ProductQuantity {get;}
    }
}