using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;

namespace InventoryFlow.Repository;

public interface IMovementRepository
{
    Task<ICollection<InventoryMovement>> GetMovementsAsync();
    Task<ICollection<InventoryMovement>> GetMovementsByStoreAsync(int idStore);
    Task<ICollection<InventoryMovement>> GetMovementsByProductAsync(int idProduct);
    Task<ICollection<MovementType>> GetMovementTypesAsync();
    Task<InventoryMovement> GetMovementAsync(int idMovement);
    Task<(bool Success, string Message)> RegisterMovementAsync(CreateMovementDto dto);
    Task<bool> SaveAsync();
}
