namespace Prueba.Domain.Entities
{
    public class Producto
    {
        public long ProductoId { get; set; }

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }

        public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
    }
}