using Microsoft.EntityFrameworkCore;
using InventoryFlow.Models;

namespace InventoryFlow.Repository;

public class InventoryRepository : IInventoryRepository
{
    private readonly AplicationDbContext _context;

    public InventoryRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<ProductInventory>> GetInventoriesAsync()
    {
        return await _context.ProductInventories
            .Include(i => i.Store)
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .OrderBy(i => i.ExpirationDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<ProductInventory>> GetInventoriesByStoreAsync(int idStore)
    {
        return await _context.ProductInventories
            .Include(i => i.Store)
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .Where(i => i.IdStore == idStore)
            .OrderBy(i => i.ExpirationDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<ProductInventory>> GetInventoriesByProductAsync(int idProduct)
    {
        return await _context.ProductInventories
            .Include(i => i.Store)
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .Where(i => i.IdProduct == idProduct)
            .OrderBy(i => i.ExpirationDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ProductInventory> GetInventoryAsync(int idInventory)
    {
        return (await _context.ProductInventories
            .Include(i => i.Store)
            .Include(i => i.Product)
                .ThenInclude(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IdInventory == idInventory))!;
    }

    public async Task<bool> InventoryExistsAsync(int idInventory)
    {
        return await _context.ProductInventories
            .AnyAsync(i => i.IdInventory == idInventory);
    }

    public async Task<bool> CreateInventoryAsync(ProductInventory inventory)
    {
        await _context.ProductInventories.AddAsync(inventory);
        return await SaveAsync();
    }

    public async Task<bool> UpdateInventoryAsync(ProductInventory inventory)
    {
        _context.ProductInventories.Update(inventory);
        return await SaveAsync();
    }

    public async Task<bool> DeleteInventoryAsync(ProductInventory inventory)
    {
        _context.ProductInventories.Remove(inventory);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
