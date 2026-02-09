using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Domain.Entities;

namespace Prueba.Application.Mappers
{
    public class OrdenProfile : Profile
    {
        public OrdenProfile()
        {
            CreateMap<OrdenCreateDto, Orden>();
            CreateMap<DetalleOrdenCreateDto, DetalleOrden>();
            CreateMap<OrdenUpdateDto, Orden>();
            CreateMap<DetalleOrdenUpdateDto, DetalleOrden>();
            CreateMap<Orden, OrdenDto>();
            CreateMap<DetalleOrden, DetalleOrdenDto>();
        }
    }
}
