using Contracts.Events;
using MassTransit;

public class ProductCreatedConsumer : IConsumer<IProductCreated>
{
    private readonly StockService _stockService;
    private readonly ILogger<ProductCreatedConsumer> _logger;

    public ProductCreatedConsumer(StockService stockService, ILogger<ProductCreatedConsumer> logger)
    {
        _stockService = stockService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IProductCreated> context)
    {
        var message = context.Message;

        await _stockService.AddStock(message.ProductId, message.ProductName, message.ProductCategory, message.CreatedAt,message.IsActive);

    }
     public async Task Update(ConsumeContext<IProductCreated> context)
    {
        var message = context.Message;

        await _stockService.DeleteStock(message.ProductId);

    }

}