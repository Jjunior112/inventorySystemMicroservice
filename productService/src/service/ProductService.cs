using Microsoft.EntityFrameworkCore;

public class ProductService
{

    private readonly ProductDbContext _context;

    public ProductService(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProducts() => await _context.Products.ToListAsync();

    public async Task<Product?> GetProductById(Guid id) => await _context.Products.FindAsync(id);

    public async Task AddProducts(Product product)
    {
        _context.Add(product);

        await _context.SaveChangesAsync();
    }

    public async Task<Product?> UpdateProduct(Guid id, UpdateProductRequest request)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null) return null;

        product.ProductName = request.productName;
        product.ProductCategory = request.productCategory;

        _context.Update(product);

        await _context.SaveChangesAsync();

        return product;

    }

    public async Task<bool> Delete(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return false;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;

    }

}