using System.ComponentModel.DataAnnotations;

namespace InventoryFlow.Models.Dtos;

public class CreateStoreDto
{
    [Required(ErrorMessage = "El nombre de la sucursal es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción de la sucursal es obligatoria.")]
    [MaxLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de Sucursal es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe especificar un tipo de sucursal válido.")]
    public int IdStoreType { get; set; }
}
