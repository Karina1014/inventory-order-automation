namespace InventoryFlow.Models.Dtos;

public class ProductDto
{
    public int IdProduct { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int IdCategory { get; set; }
    public string? CategoryName { get; set; }
    public int MinimumStockThreshold { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public int TotalStock { get; set; }
}
