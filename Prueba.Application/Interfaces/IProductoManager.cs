using Prueba.Application.DTOs;
using Prueba.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Interfaces
{
    public interface IProductoManager : IGenericManager<ProductoDto, long> {
        Task<OperationResult<ProductoDto>> CreateValidatedAsync(
                   ProductoCreateDto dto, CancellationToken ct);
        Task<OperationResult<ProductoDto>> UpdateValidatedAsync(
       int id, ProductoUpdateDto dto, CancellationToken ct);
    }
}
