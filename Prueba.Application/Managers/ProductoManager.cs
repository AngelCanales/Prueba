using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Application.Results;
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


        public async Task<OperationResult<ProductoDto>> CreateValidatedAsync(
            ProductoCreateDto dto, CancellationToken ct)
        {
            if (dto.ProductoId != 0)
                return OperationResult<ProductoDto>.Failure("El campo productoId debe ser 0");

            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length < 3 || dto.Nombre.Length > 100)
                return OperationResult<ProductoDto>.Failure("El nombre es requerido y debe tener entre 3 y 100 caracteres");

            if (dto.Precio <= 0)
                return OperationResult<ProductoDto>.Failure("El precio debe ser mayor a 0");

            if (dto.Existencia < 0)
                return OperationResult<ProductoDto>.Failure("La existencia no puede ser menor a 0");
           

            var entity = _mapper.Map<Producto>(dto);
            await _uow.Repository<Producto, int>().AddAsync(entity);
            await _uow.SaveAsync();

            var resultDto = _mapper.Map<ProductoDto>(entity);
            return OperationResult<ProductoDto>.Success(resultDto, "Producto creado exitosamente");
        }

        public async Task<OperationResult<ProductoDto>> UpdateValidatedAsync(
        int id, ProductoUpdateDto dto, CancellationToken ct)
        {
            var repo = _uow.Repository<Producto, long>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return OperationResult<ProductoDto>.Failure("Producto no encontrado");

            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length < 3 || dto.Nombre.Length > 100)
                return OperationResult<ProductoDto>.Failure("El nombre es requerido y debe tener entre 3 y 100 caracteres");

            if (dto.Precio <= 0)
                return OperationResult<ProductoDto>.Failure("El precio debe ser mayor a 0");

            if (dto.Existencia < 0)
                return OperationResult<ProductoDto>.Failure("La existencia no puede ser menor a 0");

            _mapper.Map(dto, entity);
            repo.Update(entity);
            await _uow.SaveAsync();

            var resultDto = _mapper.Map<ProductoDto>(entity);
            return OperationResult<ProductoDto>.Success(resultDto, "Producto actualizado");
        }
    }
}
