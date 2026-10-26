using InventoryFlow.Models;

namespace InventoryFlow.Repository;

public interface IProductRepository
{
    Task<ICollection<Product>> GetProductsAsync();
    Task<Product?> GetProductAsync(int idProduct);
    Task<bool> ProductExistsAsync(int idProduct);
    Task<bool> ProductExistsBySkuAsync(string sku);
    Task<bool> ProductExistsByNameAsync(string name);
    Task<bool> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);
    Task<ICollection<Category>> GetCategoriesAsync();
    Task<bool> SaveAsync();
}
