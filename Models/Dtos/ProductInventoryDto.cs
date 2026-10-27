namespace InventoryFlow.Models.Dtos;

public class ProductInventoryDto
{
    public int IdInventory { get; set; }
    public int IdStore { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public int IdProduct { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public int CurrentStock { get; set; }
    public int MinimumStockThreshold { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
}
