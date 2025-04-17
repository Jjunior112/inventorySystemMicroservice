using Contracts.Enums;

public record OperationRequest(Guid productId, int productQuantity, OperationType operationType);