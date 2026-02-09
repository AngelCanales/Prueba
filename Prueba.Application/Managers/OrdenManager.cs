using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Domain.Entities;

namespace Prueba.Application.Managers
{
    public class OrdenManager : GenericManager<Orden, OrdenDto, long>, IOrdenManager
    {
        public OrdenManager(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }
    }
}
