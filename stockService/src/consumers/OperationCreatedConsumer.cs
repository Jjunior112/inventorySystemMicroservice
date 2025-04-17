using Contracts.Events;
using MassTransit;

public class OperationCreatedConsumer : IConsumer<IOperationCreated>
{
    private readonly StockService _stockService;
    private readonly ILogger<OperationCreatedConsumer> _logger;

    public OperationCreatedConsumer(StockService stockService, ILogger<OperationCreatedConsumer> logger)
    {
        _stockService = stockService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IOperationCreated> context)
    {
        var message = context.Message;

        await _stockService.UpdateStock(message.ProductId, message.OperationType, message.Quantity);

    }

}