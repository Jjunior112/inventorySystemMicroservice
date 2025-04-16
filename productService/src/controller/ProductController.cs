using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Contracts.Events;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly ProductService _productService;
    private readonly PublishEndpoint _publishEndPoint;

    public ProductController(ProductService productService, PublishEndpoint publishEndPoint)
    {
        _productService = productService;
        _publishEndPoint = publishEndPoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts() => Ok(await _productService.GetProducts());

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
        var product = new Product(request.productName, request.productCategory);

        await _productService.AddProducts(product);



        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);

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
        return await _productService.Delete(id) ? NoContent() : NotFound();
    }

}