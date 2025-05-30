
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Contracts.Events;
using Microsoft.AspNetCore.Authorization;


[Authorize]
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

}
