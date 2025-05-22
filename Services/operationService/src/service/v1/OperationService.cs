
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

public class OperationService
{
    private readonly OperationDbContext _context;
    private readonly ICachingService _cache;
    private readonly ILogger _logger;

    public OperationService(OperationDbContext context, ICachingService cache, ILogger<Operation> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    public async Task<PagedResult<Operation>> GetOperations(int pageNumber, int pageSize)
    {
        string cacheKey = $"operations:page:{pageNumber}:size:{pageSize}";

        var cachedResult = await _cache.GetAsync<PagedResult<Operation>>(cacheKey);
        if (cachedResult != null)
        {
            _logger.LogInformation("Operações carregadas do cache!");

            return cachedResult;
        }

        _logger.LogInformation("buscando operações no banco...");

        var totalCounts = await _context.Operations.CountAsync();

        var operations = await _context.Operations.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(p => p.OperationAt).ToListAsync();

        var result = new PagedResult<Operation>
        {
            Items = operations,
            Page = pageNumber,
            PageSize = pageSize
        };

        await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;

    }

    public async Task<Operation?> GetOperationById(Guid id)
    {
        var cacheKey = $"operation:{id}";
        var operationCache = await _cache.GetAsync(cacheKey);
        Operation? operation;

        if (!string.IsNullOrWhiteSpace(operationCache))
        {
            _logger.LogInformation("Operação {OperationId} carregada do cache.", id);

            operation = JsonSerializer.Deserialize<Operation>(operationCache);

            return operation;
        }
        _logger.LogInformation("Operação {OperationId} não encontrada no cache. Buscando no banco...", id);

        operation = await _context.Operations.Where(o => o.OperationId == id).FirstOrDefaultAsync();

        if (operation == null) return null;

        await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(operation));

        return operation;
    }

    public async Task AddOperation(Operation operation)
    {

        _context.Add(operation);

        await _context.SaveChangesAsync();
    }

}