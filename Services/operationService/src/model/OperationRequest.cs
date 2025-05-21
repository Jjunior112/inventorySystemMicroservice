using Contracts.Enums;

public record OperationRequest(Guid productId,string productName,    int operationQuantity, OperationType operationType);