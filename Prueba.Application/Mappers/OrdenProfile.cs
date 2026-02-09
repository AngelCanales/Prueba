using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Domain.Entities;

namespace Prueba.Application.Mappers
{
    public class OrdenProfile : Profile
    {
        public OrdenProfile()
        {
            CreateMap<Orden, OrdenDto>().ReverseMap();
            CreateMap<DetalleOrden, DetalleOrdenDto>().ReverseMap();
        }
    }
}
