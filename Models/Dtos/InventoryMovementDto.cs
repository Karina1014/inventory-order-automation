namespace InventoryFlow.Models.Dtos;

public class InventoryMovementDto
{
    public int IdMovement { get; set; }
    public int IdMovementType { get; set; }
    public string MovementTypeName { get; set; } = string.Empty;
    public bool IsPositive { get; set; }
    public int IdProduct { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public int IdStore { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime MovementDate { get; set; }
}
