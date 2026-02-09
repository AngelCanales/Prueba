using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Domain.Entities;

namespace Prueba.Application.Managers
{
    public class ClienteManager : GenericManager<Cliente, ClienteDto, long>, IClienteManager
    {
        public ClienteManager(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }
    }
}
