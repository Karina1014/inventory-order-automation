using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryFlow.Models;

public class InventoryMovement
{
    [Key]
    public int IdMovement { get; set; }

    // Relación con el Tipo de Movimiento (Entrada, Salida, Merma, etc.)
    [Required]
    public int IdMovementType { get; set; }

    [ForeignKey(nameof(IdMovementType))]
    public MovementType? MovementType { get; set; }

    // Relación con el Producto
    [Required]
    public int IdProduct { get; set; }

    [ForeignKey(nameof(IdProduct))]
    public Product? Product { get; set; }

    // Relación con la Sucursal / Bodega
    [Required]
    public int IdStore { get; set; }

    [ForeignKey(nameof(IdStore))]
    public Store? Store { get; set; }

    // Lote afectado (para trazabilidad FEFO)
    [Required]
    [MaxLength(50)]
    public string BatchNumber { get; set; } = string.Empty;

    // Cantidad de unidades transaccionadas
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Quantity { get; set; }

    // Motivo, factura o justificación
    [Required]
    [MaxLength(250)]
    public string Reason { get; set; } = string.Empty;

    // Fecha y hora de la transacción
    [Required]
    public DateTime MovementDate { get; set; } = DateTime.UtcNow;
}
