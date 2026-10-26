using System.ComponentModel.DataAnnotations;

namespace InventoryFlow.Models.Dtos;

public class CreateProductDto
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El código SKU o de barras es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El código SKU no puede superar los 50 caracteres.")]
    public string Sku { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
    [MaxLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "La categoría del producto es obligatoria.")]
    [MaxLength(50, ErrorMessage = "La categoría no puede superar los 50 caracteres.")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "El umbral de stock mínimo es obligatorio.")]
    [Range(0, 100000, ErrorMessage = "El umbral mínimo debe ser mayor o igual a 0.")]
    public int MinimumStockThreshold { get; set; } = 5;

    [Required(ErrorMessage = "El precio unitario es obligatorio.")]
    [Range(0.01, 100000.00, ErrorMessage = "El precio unitario debe ser mayor a 0.")]
    public decimal UnitPrice { get; set; }
}
