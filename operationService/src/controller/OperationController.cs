using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OperationController : ControllerBase
{
    private readonly OperationService _operationService;

    public OperationController(OperationService operationService)
    {
        _operationService = operationService;
    }

    [HttpGet]

    public async Task<IActionResult> GetOperations() => Ok(await _operationService.GetOperations());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOperationsById(Guid id)
    {
        var operation = await _operationService.GetOperationById(id);

        if (operation == null) return NotFound();

        return Ok(operation);
    }

    [HttpPost]

    public async Task<IActionResult> AddOperation(OperationRequest request)
    {
        var operation = new Operation(request.productId, request.productQuantity, request.operationType);

        await _operationService.AddOperation(operation);

        return CreatedAtAction(nameof(GetOperationsById), new { id = operation.OperationId }, operation);
    }

    

}