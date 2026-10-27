using Microsoft.EntityFrameworkCore;
using InventoryFlow.Data;
using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;

namespace InventoryFlow.Repository;

public class MovementRepository : IMovementRepository
{
    private readonly AplicationDbContext _context;

    public MovementRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<InventoryMovement>> GetMovementsAsync()
    {
        return await _context.InventoryMovements
            .Include(m => m.MovementType)
            .Include(m => m.Product)
            .Include(m => m.Store)
            .OrderByDescending(m => m.MovementDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<InventoryMovement>> GetMovementsByStoreAsync(int idStore)
    {
        return await _context.InventoryMovements
            .Include(m => m.MovementType)
            .Include(m => m.Product)
            .Include(m => m.Store)
            .Where(m => m.IdStore == idStore)
            .OrderByDescending(m => m.MovementDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<InventoryMovement>> GetMovementsByProductAsync(int idProduct)
    {
        return await _context.InventoryMovements
            .Include(m => m.MovementType)
            .Include(m => m.Product)
            .Include(m => m.Store)
            .Where(m => m.IdProduct == idProduct)
            .OrderByDescending(m => m.MovementDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<MovementType>> GetMovementTypesAsync()
    {
        return await _context.MovementTypes
            .Where(m => m.IsActive)
            .OrderBy(m => m.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<InventoryMovement> GetMovementAsync(int idMovement)
    {
        return (await _context.InventoryMovements
            .Include(m => m.MovementType)
            .Include(m => m.Product)
            .Include(m => m.Store)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.IdMovement == idMovement))!;
    }

    public async Task<(bool Success, string Message)> RegisterMovementAsync(CreateMovementDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var movementType = await _context.MovementTypes.FirstOrDefaultAsync(m => m.IdMovementType == dto.IdMovementType);
            if (movementType == null)
            {
                return (false, "El tipo de movimiento seleccionado no existe.");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.IdProduct == dto.IdProduct);
            if (product == null)
            {
                return (false, "El producto seleccionado no existe.");
            }

            var store = await _context.Stores.FirstOrDefaultAsync(s => s.IdStore == dto.IdStore);
            if (store == null)
            {
                return (false, "La sucursal seleccionada no existe.");
            }

            string assignedBatch = string.IsNullOrWhiteSpace(dto.BatchNumber) ? string.Empty : dto.BatchNumber.Trim();

            // 1. CASO ENTRADA / INGRESO (+)
            if (movementType.IsPositive)
            {
                if (string.IsNullOrWhiteSpace(assignedBatch))
                {
                    assignedBatch = "LOT-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmm");
                }

                var existingLot = await _context.ProductInventories
                    .FirstOrDefaultAsync(i => i.IdStore == dto.IdStore && i.IdProduct == dto.IdProduct && i.BatchNumber == assignedBatch);

                if (existingLot != null)
                {
                    existingLot.CurrentStock += dto.Quantity;
                    existingLot.ExpirationDate = dto.ExpirationDate;
                    _context.ProductInventories.Update(existingLot);
                }
                else
                {
                    var newLot = new ProductInventory
                    {
                        IdStore = dto.IdStore,
                        IdProduct = dto.IdProduct,
                        BatchNumber = assignedBatch,
                        ExpirationDate = dto.ExpirationDate,
                        CurrentStock = dto.Quantity,
                        IsActive = true,
                        CreationDate = DateTime.UtcNow
                    };
                    await _context.ProductInventories.AddAsync(newLot);
                }
            }
            // 2. CASO SALIDA / MERMA / DISPENSACIÓN (-)
            else
            {
                var availableLots = await _context.ProductInventories
                    .Where(i => i.IdStore == dto.IdStore && i.IdProduct == dto.IdProduct && i.CurrentStock > 0)
                    .OrderBy(i => i.ExpirationDate) // Prioridad FEFO: lote que expira primero
                    .ToListAsync();

                int totalStockAvailable = availableLots.Sum(i => i.CurrentStock);
                if (totalStockAvailable < dto.Quantity)
                {
                    return (false, $"Stock insuficiente en {store.Name}. Stock disponible: {totalStockAvailable} u. (solicitadas: {dto.Quantity} u.).");
                }

                // Si especificó lote puntual
                if (!string.IsNullOrWhiteSpace(assignedBatch))
                {
                    var specificLot = availableLots.FirstOrDefault(i => i.BatchNumber == assignedBatch);
                    if (specificLot == null || specificLot.CurrentStock < dto.Quantity)
                    {
                        int currentAvailable = specificLot != null ? specificLot.CurrentStock : 0;
                        return (false, $"El lote '{assignedBatch}' no tiene suficiente stock ({currentAvailable} u. disponibles).");
                    }

                    specificLot.CurrentStock -= dto.Quantity;
                    _context.ProductInventories.Update(specificLot);
                }
                // Si no especificó lote, aplicar despacho inteligente FEFO automático
                else
                {
                    int pendingQuantity = dto.Quantity;
                    var lotsUsedList = new List<string>();

                    foreach (var lot in availableLots)
                    {
                        if (pendingQuantity <= 0) break;

                        if (lot.CurrentStock >= pendingQuantity)
                        {
                            lot.CurrentStock -= pendingQuantity;
                            lotsUsedList.Add($"{lot.BatchNumber} ({pendingQuantity} u.)");
                            pendingQuantity = 0;
                            _context.ProductInventories.Update(lot);
                        }
                        else
                        {
                            pendingQuantity -= lot.CurrentStock;
                            lotsUsedList.Add($"{lot.BatchNumber} ({lot.CurrentStock} u.)");
                            lot.CurrentStock = 0;
                            _context.ProductInventories.Update(lot);
                        }
                    }

                    assignedBatch = string.Join(", ", lotsUsedList);
                }
            }

            // 3. Registrar el asiento histórico en el Kárdex
            var movement = new InventoryMovement
            {
                IdMovementType = dto.IdMovementType,
                IdProduct = dto.IdProduct,
                IdStore = dto.IdStore,
                BatchNumber = assignedBatch,
                Quantity = dto.Quantity,
                Reason = dto.Reason.Trim(),
                MovementDate = DateTime.UtcNow
            };

            await _context.InventoryMovements.AddAsync(movement);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return (true, "Movimiento registrado exitosamente en el Kárdex.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error interno al registrar movimiento: {ex.Message}");
        }
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
