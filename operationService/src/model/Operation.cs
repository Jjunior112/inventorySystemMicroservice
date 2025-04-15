public class Operation
{
    public Guid OperationId { get; private set; }

    public Guid ProductId { get; set; }

    public int OperationQuantity { get; set; }

    public OperationType OperationType { get; set; }

    public Operation(Guid productId, int operationQuantity, OperationType operationType)
    {
        OperationId = Guid.NewGuid();
        ProductId = productId;
        OperationQuantity = operationQuantity;
        OperationType = operationType;

    }
}