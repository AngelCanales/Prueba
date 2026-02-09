using Prueba.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Interfaces
{
    public interface IProductoManager : IGenericManager<ProductoDto, long> { }
}
