namespace InventoryFlow.Models.Dtos;

public class MovementTypeDto
{
    public int IdMovementType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPositive { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
}
