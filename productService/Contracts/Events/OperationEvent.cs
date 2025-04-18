using Contracts.Enums;

namespace Contracts.Events
{
    public interface IOperationCreated
    {
        Guid ProductId { get; }

        OperationType OperationType { get; }

        int Quantity { get; }


    }
}