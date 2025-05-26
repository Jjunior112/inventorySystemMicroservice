using Contracts.Enums;

namespace Contracts.Events
{
    public interface IOperationCreated
    {
        Guid ProductId { get; }

        string ProductName { get; }

        OperationType OperationType { get; }

        int Quantity { get; }


    }
}