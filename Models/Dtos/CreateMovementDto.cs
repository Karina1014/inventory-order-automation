using System.ComponentModel.DataAnnotations;

namespace InventoryFlow.Models.Dtos;

public class CreateMovementDto
{
    [Required(ErrorMessage = "Debe seleccionar un tipo de movimiento.")]
    [Range(1, int.MaxValue, ErrorMessage = "Tipo de movimiento no válido.")]
    public int IdMovementType { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un producto.")]
    [Range(1, int.MaxValue, ErrorMessage = "Producto no válido.")]
    public int IdProduct { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una sucursal / bodega.")]
    [Range(1, int.MaxValue, ErrorMessage = "Sucursal no válida.")]
    public int IdStore { get; set; }

    [MaxLength(50, ErrorMessage = "El lote no puede superar 50 caracteres.")]
    public string BatchNumber { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddMonths(12);

    [Required(ErrorMessage = "Debe especificar la cantidad.")]
    [Range(1, 100000, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "El motivo o justificación es obligatorio.")]
    [MaxLength(250, ErrorMessage = "El motivo no puede superar los 250 caracteres.")]
    public string Reason { get; set; } = string.Empty;
}
