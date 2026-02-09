namespace Prueba.Application.Interfaces
{
    using Prueba.Application.Results;
    using System.Threading;

    public interface IGenericManager<TDto, TKey>
    {
        Task<OperationResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken ct = default);
        Task<OperationResult<TDto>> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<OperationResult<TDto>> CreateAsync(TDto dto, CancellationToken ct = default);
        Task<OperationResult<TDto>> UpdateAsync(TKey id, TDto dto, CancellationToken ct = default);
        Task<OperationResult<bool>> DeleteAsync(TKey id, CancellationToken ct = default);
    }
}
