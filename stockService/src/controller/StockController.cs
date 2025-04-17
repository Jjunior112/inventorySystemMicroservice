using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly StockService _stockService;


    public StockController(StockService stockService)
    {
        _stockService = stockService;
    }
    [HttpGet]
    public async Task<IActionResult> GetStocks() => Ok(await _stockService.GetStocks());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStocksById(Guid id) => Ok(await _stockService.GetStockById(id));

}