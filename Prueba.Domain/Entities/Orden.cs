namespace Prueba.Domain.Entities
{
    public class Orden
    {
        public long OrdenId { get; set; }

        public long ClienteId { get; set; }

        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public Cliente Cliente { get; set; } = null!;
        public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
    }
}