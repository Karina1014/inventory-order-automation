using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryFlow.Models;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;

    // Relación / Clave Foránea hacia Categoría
    [Required]
    public int IdCategory { get; set; }

    [ForeignKey(nameof(IdCategory))]
    public Category? Category { get; set; }

    [Required]
    public int MinimumStockThreshold { get; set; } = 5;

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
}
