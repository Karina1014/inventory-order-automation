using InventoryFlow.Models;

namespace InventoryFlow.Repository;

public interface IInventoryRepository
{
    Task<ICollection<ProductInventory>> GetInventoriesAsync();
    Task<ICollection<ProductInventory>> GetInventoriesByStoreAsync(int idStore);
    Task<ICollection<ProductInventory>> GetInventoriesByProductAsync(int idProduct);
    Task<ProductInventory> GetInventoryAsync(int idInventory);
    Task<bool> InventoryExistsAsync(int idInventory);
    Task<bool> CreateInventoryAsync(ProductInventory inventory);
    Task<bool> UpdateInventoryAsync(ProductInventory inventory);
    Task<bool> DeleteInventoryAsync(ProductInventory inventory);
    Task<bool> SaveAsync();
}
