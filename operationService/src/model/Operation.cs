using Contracts.Enums;

public class Operation
{
    public Guid OperationId { get; private set; }

    public Guid ProductId { get; set; }

    public int OperationQuantity { get; set; }

    public OperationType OperationType { get; set; }

    public DateTime OperationAt { get; private set; }

    public Operation(Guid productId, int operationQuantity, OperationType operationType)
    {
        OperationId = Guid.NewGuid();
        ProductId = productId;
        OperationQuantity = operationQuantity;
        OperationType = operationType;
        OperationAt = DateTime.Now;

    }
}