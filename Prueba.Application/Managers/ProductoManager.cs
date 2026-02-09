using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Managers
{
    public class ProductoManager : GenericManager<Producto, ProductoDto, long>, IProductoManager
    {
        public ProductoManager(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }
    }
}
