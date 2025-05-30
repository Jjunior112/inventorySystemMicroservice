using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Microsoft.AspNetCore.Authorization;


[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/products")]
public class ProductController : ControllerBase
{

    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;

    }

    [HttpGet()]
    public async Task<IActionResult> GetProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var products = await _productService.GetProducts(pageNumber, pageSize);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductById(id);

        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductRequest request)
    {

        var product = await _productService.AddProducts(request);

        if (product != null) return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);


        return BadRequest();

    }
    [HttpPatch("{id}")]

    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
    {
        var product = await _productService.UpdateProduct(id, request);

        if (product == null) return NotFound();

        return Ok(product);

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _productService.GetProductById(id);

        if (product != null)
        {
            var productDeleted = await _productService.Delete(product.ProductId);

            if (productDeleted) return NoContent();

        }
        return NotFound();
    }
}
