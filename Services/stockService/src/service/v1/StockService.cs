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
        string cacheKey = $"stocks:page:{pageNumber}:size:{pageSize}";

        var cachedResult = await _cache.GetAsync<PagedResult<Stock>>(cacheKey);
        if (cachedResult != null)
        {
            _logger.LogInformation("Estoque carregado do cache!");
            return cachedResult;
        }

        _logger.LogInformation("buscando estoque no banco...");

        var totalCounts = await _context.Stocks.CountAsync();

        var stocks = await _context.Stocks.ToListAsync();

        var result = new PagedResult<Stock>
        {
            Items = stocks,
            Page = pageNumber,
            PageSize = pageSize
        };

        await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
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

    public async Task AddStock(Guid productId, string productName, string productCategory, DateTime createdAt)
    {

        var stock = new Stock(productId, productName, productCategory, createdAt);


        _context.Stocks.Add(stock);

        await _context.SaveChangesAsync();
    }

    public async Task<Stock?> UpdateStock(Guid id, OperationType operationType, int productQuantity)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == id);

        if (stock == null) return null;

        switch (operationType)
        {
            case OperationType.StockOut:

                if (productQuantity <= 0)
                {
                    return null;
                }

                if (productQuantity <= stock.ProductQuantity)
                {
                    stock.ProductQuantity -= productQuantity;
                }
                else
                {

                    return null;
                }
                break;

            case OperationType.StockIn:
                if (productQuantity <= 0)
                {
                    return null;
                }
                stock.ProductQuantity += productQuantity;
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

        _context.Stocks.Remove(stock);

        await _cache.DeleteAsync(id.ToString());

        await _context.SaveChangesAsync();

        return true;
    }
}