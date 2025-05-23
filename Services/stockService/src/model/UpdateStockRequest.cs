using Contracts.Enums;

public record UpdateStockRequest(Guid productId, int operationQuantity, OperationType operationType);