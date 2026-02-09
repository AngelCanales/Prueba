using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.DTOs
{
    public class ProductoDto
    {
        public long ProductoId { get; set; }

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
    }


    public class ProductoUpdateDto
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
    }

    public class ProductoCreateDto
    {
        public long ProductoId { get; set; }

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
    }
}
