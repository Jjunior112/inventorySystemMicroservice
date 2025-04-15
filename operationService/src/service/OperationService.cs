using Microsoft.EntityFrameworkCore;

public class OperationService
{
    private readonly OperationDbContext _context;

    public OperationService(OperationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Operation>> GetOperations() => await _context.Operations.ToListAsync();

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