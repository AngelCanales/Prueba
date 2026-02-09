using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Domain.Entities;

namespace Prueba.Application.Mappers
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<ProductoCreateDto, Producto>();

            CreateMap<ProductoUpdateDto, Producto>();

            CreateMap<Producto, ProductoDto>();
        }
    }
}
