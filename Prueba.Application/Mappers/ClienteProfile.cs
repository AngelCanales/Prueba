using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Domain.Entities;


namespace Prueba.Application.Mappers
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteCreateDto, Cliente>();

            CreateMap<ClienteUpdateDto, Cliente>();

            CreateMap<Cliente, ClienteDto>();
        }
    }
}
