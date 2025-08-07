namespace Productos.Common.Dto
{
    public class ProductoDto
    {
        public int? CodigoProducto { get; set; }
        public string? Nombre { get; set; } = null!;
        public string? Descripcion { get; set; } = null!;
        public string? ReferenciaInterna { get; set; } = null!;
        public decimal? PrecioUnitario { get; set; }
        public bool? Estado { get; set; }
        public string? UnidadMedida { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
