using Microsoft.EntityFrameworkCore;
using InventoryFlow.Models;

namespace InventoryFlow.Repository;

public class StoreRepository : IStoreRepository
{
    private readonly AplicationDbContext _context;

    public StoreRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Store>> GetStoresAsync()
    {
        return await _context.Stores
            .Include(s => s.StoreType)
            .OrderBy(s => s.IdStore)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Store?> GetStoreAsync(int idStore)
    {
        return await _context.Stores
            .Include(s => s.StoreType)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.IdStore == idStore);
    }

    public async Task<bool> StoreExistsAsync(int idStore)
    {
        return await _context.Stores
            .AnyAsync(s => s.IdStore == idStore);
    }

    public async Task<bool> StoreExistsAsync(string name)
    {
        return await _context.Stores
            .AnyAsync(s => s.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<bool> CreateStoreAsync(Store store)
    {
        await _context.Stores.AddAsync(store);
        return await SaveAsync();
    }

    public async Task<bool> UpdateStoreAsync(Store store)
    {
        _context.Stores.Update(store);
        return await SaveAsync();
    }

    public async Task<bool> DeleteStoreAsync(Store store)
    {
        _context.Stores.Remove(store);
        return await SaveAsync();
    }

    public async Task<ICollection<StoreType>> GetStoreTypesAsync()
    {
        return await _context.StoreTypes
            .OrderBy(st => st.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
