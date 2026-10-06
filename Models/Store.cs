using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryFlow.Models;

public class Store
{
    [Key]
    public int IdStore { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    // (CLAVE FORÁNEA) 
    [Required]
    public int IdStoreType { get; set; }

    [ForeignKey(nameof(IdStoreType))]
    public StoreType StoreType { get; set; } = null!;
}
