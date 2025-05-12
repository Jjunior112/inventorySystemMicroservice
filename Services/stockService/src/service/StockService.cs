using Microsoft.EntityFrameworkCore;
using Contracts.Enums;


public class StockService
{
    private readonly StockDbContext _context;

    public StockService(StockDbContext context)
    {
        _context = context;
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

    public async Task<Stock?> GetStockById(Guid id) => await _context.Stocks.FindAsync(id);

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

    public async Task<bool?> Delete(Guid id)
    {
        var stock = await _context.Stocks.FindAsync(id);

        if (stock == null) return null;

        if (stock.ProductQuantity > 0) return false;

        _context.Stocks.Remove(stock);

        await _context.SaveChangesAsync();

        return true;
    }
}