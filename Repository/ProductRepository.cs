using Microsoft.EntityFrameworkCore;
using InventoryFlow.Models;

namespace InventoryFlow.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AplicationDbContext _context;

    public ProductRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductInventories)
            .OrderBy(p => p.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product> GetProductAsync(int idProduct)
    {
        return (await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductInventories)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.IdProduct == idProduct))!;
    }

    public async Task<bool> ProductExistsAsync(int idProduct)
    {
        return await _context.Products
            .AnyAsync(p => p.IdProduct == idProduct);
    }

    public async Task<bool> ProductExistsBySkuAsync(string sku)
    {
        return await _context.Products
            .AnyAsync(p => p.Sku.ToLower().Trim() == sku.ToLower().Trim());
    }

    public async Task<bool> ProductExistsByNameAsync(string name)
    {
        return await _context.Products
            .AnyAsync(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<bool> CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        return await SaveAsync();
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        return await SaveAsync();
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        return await SaveAsync();
    }

    public async Task<ICollection<Category>> GetCategoriesAsync()
    {
        return await _context.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
