using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryFlow.Models;

public class ProductInventory
{
    [Key]
    public int IdInventory { get; set; }

    [Required]
    public int IdStore { get; set; }

    [ForeignKey(nameof(IdStore))]
    public Store? Store { get; set; }

    [Required]
    public int IdProduct { get; set; }

    [ForeignKey(nameof(IdProduct))]
    public Product? Product { get; set; }

    [Required]
    [MaxLength(50)]
    public string BatchNumber { get; set; } = string.Empty;

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    public int CurrentStock { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
