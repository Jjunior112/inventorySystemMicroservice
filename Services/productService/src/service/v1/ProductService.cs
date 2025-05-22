
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

public class ProductService
{

    private readonly ProductDbContext _context;
    private readonly ICachingService _cache;

    private readonly ILogger _logger;

    public ProductService(ProductDbContext context, ICachingService cache, ILogger<Product> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;

    }

    public async Task<PagedResult<Product>> GetProducts(int pageNumber, int pageSize)
    {
        string cacheKey = $"products:page:{pageNumber}:size:{pageSize}";

        var cachedResult = await _cache.GetAsync<PagedResult<Product>>(cacheKey);
        if (cachedResult != null)
        {
            _logger.LogInformation("Produtos carregados do cache!");
            return cachedResult;
        }

        _logger.LogInformation("buscando produtos no banco...");

        var totalCounts = await _context.Products.CountAsync();

        var products = await _context.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(p => p.CreatedAt).ToListAsync();

        var result = new PagedResult<Product>
        {
            Items = products,
            TotalCounts = totalCounts,
            Page = pageNumber,
            PageSize = pageSize
        };

        await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;

    }

    public async Task<Product?> GetProductById(Guid id)
    {
        var cacheKey = $"product: {id}";

        var productCache = await _cache.GetAsync(cacheKey);

        Product? product;

        if (!string.IsNullOrWhiteSpace(productCache))
        {
            _logger.LogInformation($"produto {id} carregado do cache");

            product = JsonSerializer.Deserialize<Product>(productCache);

            return product;

        }

        _logger.LogInformation($"produto {id} não encontrado no cache. buscando no banco...");

        product = await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

        await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(product));

        return product;
    }

    public async Task AddProducts(Product product)
    {
        _context.Add(product);

        await _context.SaveChangesAsync();
    }

    public async Task<Product?> UpdateProduct(Guid id, UpdateProductRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return null;

        product.ProductName = request.productName;
        product.ProductCategory = request.productCategory;

        _context.Update(product);

        await _context.SaveChangesAsync();

        return product;

    }

    public async Task<bool> Delete(Guid id)
    {
        var cacheKey = $"product: {id}";

        var product = await _context.Products.FindAsync(id);

        if (product == null) return false;

        var productCache = await _cache.GetAsync(cacheKey);

        if (productCache != null)
        {
            await _cache.DeleteAsync(cacheKey);
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;

    }

}
