using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Entities
{
    public class Cliente
    {
        public long ClienteId { get; set; }

        public string Nombre { get; set; } = null!;
        public string Identidad { get; set; } = null!;

        public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
    }
}
