using Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.DTOs
{
    public class OrdenDto
    {
        public long OrdenId { get; set; }

        public long ClienteId { get; set; }

        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public List<DetalleOrdenDto> Detalles { get; set; } = new();

    }
}
