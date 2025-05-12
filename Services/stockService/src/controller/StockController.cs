using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stocks")]
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

}