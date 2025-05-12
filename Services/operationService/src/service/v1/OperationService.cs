
using Microsoft.EntityFrameworkCore;

public class OperationService
{
    private readonly OperationDbContext _context;

    public OperationService(OperationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Operation>> GetOperations(int pageNumber, int pageSize)
    {
        var totalCounts = await _context.Operations.CountAsync();

        var operations = await _context.Operations.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(p => p.OperationAt).ToListAsync();

        return new PagedResult<Operation>
        {
            Items = operations,
            Page = pageNumber,
            PageSize = pageSize
        };

    }

    public async Task<Operation?> GetOperationById(Guid id)
    {
        var operation = await _context.Operations.FindAsync(id);

        if (operation == null) return null;

        return operation;
    }

    public async Task AddOperation(Operation operation)
    {

        _context.Operations.Add(operation);

        await _context.SaveChangesAsync();
    }

}