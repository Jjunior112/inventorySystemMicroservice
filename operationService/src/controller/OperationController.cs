using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Contracts.Events;

[ApiController]
[Route("api/operations")]
public class OperationController : ControllerBase
{
    private readonly OperationService _operationService;
    private readonly IPublishEndpoint _publishEndPoint;

    public OperationController(OperationService operationService, IPublishEndpoint publishEndpoint)
    {
        _operationService = operationService;
        _publishEndPoint = publishEndpoint;
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

    public async Task<IActionResult> AddOperation([FromBody] OperationRequest request)
    {

        var operation = new Operation(request.productId, request.operationQuantity, request.operationType);


        await _publishEndPoint.Publish<IOperationCreated>(new
        {
            ProductId = request.productId,
            OperationType = request.operationType,
            Quantity = request.operationQuantity
        },
        CancellationToken.None
        );

        await _operationService.AddOperation(operation);

        return CreatedAtAction(nameof(GetOperationsById), new { id = operation.OperationId }, operation);

    }



}