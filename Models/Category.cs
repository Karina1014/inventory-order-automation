using System.ComponentModel.DataAnnotations;

namespace InventoryFlow.Models;

public class Category
{
    [Key]
    public int IdCategory { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
