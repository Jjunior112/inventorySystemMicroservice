using Microsoft.EntityFrameworkCore;
using Contracts.Enums;
using System.Text.Json;


public class StockService
{
    private readonly StockDbContext _context;

    private readonly ICachingService _cache;
    private readonly ILogger _logger;


    public StockService(StockDbContext context, ICachingService cache, ILogger<Stock> logger)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    public async Task<PagedResult<Stock>> GetStocks(int pageNumber, int pageSize)
    {

        var totalCounts = await _context.Stocks.CountAsync();

        var stocks = await _context.Stocks.ToListAsync();

        return new PagedResult<Stock>
        {
            Items = stocks,
            Page = pageNumber,
            PageSize = pageSize
        };


    }

    public async Task<Stock?> GetStockById(Guid id)
    {
        var cacheKey = $"stock:{id}";

        var stockCache = await _cache.GetAsync(cacheKey);

        Stock? stock;

        if (!string.IsNullOrWhiteSpace(stockCache))
        {
            _logger.LogInformation($"Stock {id} carreado do cache");

            stock = JsonSerializer.Deserialize<Stock>(stockCache);

            return stock;
        }

        _logger.LogInformation($"Stock {id} não encontrado no cache.Buscando no banco...");

        stock = await _context.Stocks.Where(s => s.StockId == id).FirstOrDefaultAsync();

        await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(stock));


        return stock;
    }

    public async Task AddStock(Guid productId, string productName, string productCategory, DateTime createdAt, bool isActive)
    {

        var stock = new Stock(productId, productName, productCategory, createdAt, isActive);


        _context.Stocks.Add(stock);

        await _context.SaveChangesAsync();
    }

    public async Task<Stock?> UpdateStock(UpdateStockRequest request)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == request.productId);

        if (stock == null) return null;

        switch (request.operationType)
        {
            case OperationType.StockOut:

                if (request.operationQuantity <= 0 || !stock.IsActive)
                {
                    return null;
                }

                if (request.operationQuantity <= stock.ProductQuantity)
                {
                    stock.ProductQuantity -= request.operationQuantity;
                }
                else
                {

                    return null;
                }
                break;

            case OperationType.StockIn:
                if (request.operationQuantity <= 0)
                {
                    return null;
                }
                stock.ProductQuantity += request.operationQuantity;
                break;
        }

        _context.Update(stock);

        await _context.SaveChangesAsync();

        return stock;

    }

    public async Task<bool?> DeleteStock(Guid id)
    {
        var stock = await _context.Stocks.FindAsync(id);

        if (stock == null) return null;

        if (stock.ProductQuantity > 0) return false;

        stock.IsActive = false;

        _context.Stocks.Update(stock);

        await _cache.DeleteAsync(id.ToString());

        await _context.SaveChangesAsync();

        return true;
    }
}