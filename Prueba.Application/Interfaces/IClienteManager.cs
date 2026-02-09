using Prueba.Application.DTOs;
using Prueba.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Interfaces
{
    public interface IClienteManager : IGenericManager<ClienteDto, long> {
        Task<OperationResult<ClienteDto>> CreateValidatedAsync(
                ClienteCreateDto dto, CancellationToken ct);
        Task<OperationResult<ClienteDto>> UpdateValidatedAsync(
       int id, ClienteUpdateDto dto, CancellationToken ct);
    }
}
