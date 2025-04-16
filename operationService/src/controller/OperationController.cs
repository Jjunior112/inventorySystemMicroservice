using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Contracts.Events;



[ApiController]
[Route("api/[controller]")]
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
        var operation = new Operation(request.productId, request.productQuantity, request.operationType);

        await _operationService.AddOperation(operation);

        await _publishEndPoint.Publish(new OperationEvent(
            request.productId, request.productQuantity

        ));

        return CreatedAtAction(nameof(GetOperationsById), new { id = operation.OperationId }, operation);
    }



}