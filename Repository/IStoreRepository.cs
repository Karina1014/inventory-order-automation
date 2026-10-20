using InventoryFlow.Models;

namespace InventoryFlow.Repository;

public interface IStoreRepository
{
    //Son los metodos que se van a implementar en el StoreController
    Task<ICollection<Store>> GetStoresAsync();
    Task<Store?> GetStoreAsync(int idStore);
    Task<bool> StoreExistsAsync(int idStore);
    Task<bool> StoreExistsAsync(string name);
    Task<bool> CreateStoreAsync(Store store);
    Task<bool> UpdateStoreAsync(Store store);
    Task<bool> DeleteStoreAsync(Store store);
    Task<bool> SaveAsync();
}
