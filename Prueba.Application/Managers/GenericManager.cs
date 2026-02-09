namespace Prueba.Application.Managers
{
    using AutoMapper;
    using Prueba.Application.Interfaces;
    using Prueba.Application.Results;
    using System.Threading;

    public class GenericManager<TEntity, TDto, TKey> : IGenericManager<TDto, TKey>
        where TEntity : class
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;

        public GenericManager(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public virtual async Task<OperationResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken ct = default)
        {
            var data = await _uow.Repository<TEntity, TKey>().GetAllAsync();
            var dto = _mapper.Map<IEnumerable<TDto>>(data);
            return OperationResult<IEnumerable<TDto>>.Success(dto);
        }

        public virtual async Task<OperationResult<TDto>> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            var entity = await _uow.Repository<TEntity, TKey>().GetByIdAsync(id);
            if (entity == null)
                return OperationResult<TDto>.Failure("Not found");

            var dto = _mapper.Map<TDto>(entity);
            return OperationResult<TDto>.Success(dto);
        }

        public virtual async Task<OperationResult<TDto>> CreateAsync(TDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _uow.Repository<TEntity, TKey>().AddAsync(entity);
            await _uow.SaveAsync();
            return OperationResult<TDto>.Success(dto, "Created successfully");
        }

        public virtual async Task<OperationResult<TDto>> UpdateAsync(TKey id, TDto dto, CancellationToken ct = default)
        {
            var repo = _uow.Repository<TEntity, TKey>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                return OperationResult<TDto>.Failure("Not found");

            _mapper.Map(dto, entity);
            repo.Update(entity);
            await _uow.SaveAsync();
            return OperationResult<TDto>.Success(dto, "Updated successfully");
        }

        public virtual async Task<OperationResult<bool>> DeleteAsync(TKey id, CancellationToken ct = default)
        {
            var repo = _uow.Repository<TEntity, TKey>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                return OperationResult<bool>.Failure("Not found");

            repo.Remove(entity);
            await _uow.SaveAsync();
            return OperationResult<bool>.Success(true, "Deleted successfully");
        }
    }

}
