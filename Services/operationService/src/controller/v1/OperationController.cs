
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Contracts.Events;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/operations")]
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

    public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var operations = await _operationService.GetOperations(pageNumber, pageSize);

        return Ok(operations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var operation = await _operationService.GetOperationById(id);

        if (operation == null) return NotFound();

        return Ok(operation);
    }

    [HttpPost]

    public async Task<IActionResult> Post([FromBody] OperationRequest request)
    {

        var operation = new Operation(request.productId,request.productName, request.operationQuantity, request.operationType);

        await _operationService.AddOperation(operation);

        await _publishEndPoint.Publish<IOperationCreated>(new
        {
            ProductId = request.productId,
            OperationType = request.operationType,
            Quantity = request.operationQuantity
        },
        CancellationToken.None
        );


        return CreatedAtAction(nameof(GetById), new { id = operation.OperationId }, operation);

    }



}
