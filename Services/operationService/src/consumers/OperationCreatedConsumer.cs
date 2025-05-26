using Contracts.Events;
using MassTransit;

public class OperationCreatedConsumer : IConsumer<IOperationCreated>
{
    private readonly OperationService _operationService;
    private readonly ILogger<OperationCreatedConsumer> _logger;

    public OperationCreatedConsumer(OperationService operationService, ILogger<OperationCreatedConsumer> logger)
    {
        _operationService = operationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IOperationCreated> context)
    {
        var message = context.Message;

        await _operationService.AddOperation(message.ProductId, message.ProductName, message.Quantity, message.OperationType);

    }

}