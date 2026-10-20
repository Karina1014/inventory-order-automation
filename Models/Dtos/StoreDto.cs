namespace InventoryFlow.Models.Dtos;

public class StoreDto
{
    // Atributos del DTO de Sucursal
    public int IdStore { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public int IdStoreType { get; set; }
    public string? StoreTypeName { get; set; }
}
