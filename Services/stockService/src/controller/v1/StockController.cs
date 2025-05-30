
using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/stocks")]
public class StockController : ControllerBase
{
    private readonly StockService _stockService;



    public StockController(StockService stockService)
    {
        _stockService = stockService;

    }
    [HttpGet]
    public async Task<IActionResult> GetStocks([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var stocks = await _stockService.GetStocks(pageNumber, pageSize);

        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStocksById(Guid id) => Ok(await _stockService.GetStockById(id));


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UpdateStockRequest request)
    {

        var updateStock = await _stockService.UpdateStock(request);

        if (updateStock == null) return BadRequest();

        return Ok();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock(Guid id)
    {
        var stock = await _stockService.DeleteStock(id);

        if (stock == null || false) return NotFound();



        return NoContent();
    }

}
