using System.ComponentModel.DataAnnotations;

namespace InventoryFlow.Models;

public class StoreType
{
    [Key]
    public int IdStoreType {get;set;}
    [Required]
    [MaxLength(250)]
    public string Name {get; set;} = string.Empty; // "Clinic", "PetShop", "Hybrid"
    [Required]
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public bool IsActive { get; set; } = true;
    [Required]
    public DateTime CreationDate {get;set;}
}
