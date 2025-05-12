using Contracts.Enums;

public record OperationRequest(Guid productId, int operationQuantity, OperationType operationType);