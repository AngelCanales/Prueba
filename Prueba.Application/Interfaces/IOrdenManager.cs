using Prueba.Application.DTOs;
using Prueba.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Interfaces
{
    public interface IOrdenManager : IGenericManager<OrdenDto, long> {

        Task<OperationResult<OrdenDto>> CreateValidatedAsync(
               OrdenCreateDto dto, CancellationToken ct);
        Task<OperationResult<OrdenDto>> UpdateValidatedAsync(
       int id, OrdenUpdateDto dto, CancellationToken ct);
    }
}
