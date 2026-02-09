using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Application.Results;
using Prueba.Domain.Entities;

namespace Prueba.Application.Managers
{
    public class ClienteManager : GenericManager<Cliente, ClienteDto, long>, IClienteManager
    {
        public ClienteManager(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }


        public async Task<OperationResult<ClienteDto>> CreateValidatedAsync(
            ClienteCreateDto dto, CancellationToken ct)
        {
            // identidad única
            var all = await _uow.Repository<Cliente, long>().GetAllAsync();

            var exists = all.Any(c => c.Identidad == dto.Identidad);

            if (exists)
                return OperationResult<ClienteDto>.Failure(
                    $"Ya existe un cliente con la identidad {dto.Identidad}");

            var entity = _mapper.Map<Cliente>(dto);
            await _uow.Repository<Cliente, int>().AddAsync(entity);
            await _uow.SaveAsync();

            var resultDto = _mapper.Map<ClienteDto>(entity);
            return OperationResult<ClienteDto>.Success(resultDto, "Cliente creado exitosamente");
        }

        public async Task<OperationResult<ClienteDto>> UpdateValidatedAsync(
        int id, ClienteUpdateDto dto, CancellationToken ct)
        {
            var repo = _uow.Repository<Cliente, long>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return OperationResult<ClienteDto>.Failure("Cliente no encontrado");

            // identidad única excepto él mismo
            var exists = await repo.AnyAsync(c =>
                c.Identidad == dto.Identidad && c.ClienteId != id);

            if (exists)
                return OperationResult<ClienteDto>.Failure(
                    $"Ya existe otro cliente con la identidad {dto.Identidad}");

            _mapper.Map(dto, entity);
            repo.Update(entity);
            await _uow.SaveAsync();

            var resultDto = _mapper.Map<ClienteDto>(entity);
            return OperationResult<ClienteDto>.Success(resultDto, "Cliente actualizado");
        }
    }
}
