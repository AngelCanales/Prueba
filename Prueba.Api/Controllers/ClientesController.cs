using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Application.Results;

namespace Prueba.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteManager _manager;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IClienteManager manager, ILogger<ClientesController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            _logger.LogInformation("Obteniendo clientes");
            var result = await _manager.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _manager.GetByIdAsync(id, ct);

            if (!result.Succeeded)
            {
                var error = OperationResult<ClienteDto>.Failure("Error al crear el cliente", ["No existe un cliente con el ID especificado"]);
                return NotFound(error);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ClienteCreateDto dto, CancellationToken ct)
        {
            if (dto.ClienteId != 0)
                return BadRequest("clienteId debe ser 0");

            var result = await _manager.CreateValidatedAsync(dto, ct);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id, [FromBody] ClienteUpdateDto dto, CancellationToken ct)
        {
            var result = await _manager.UpdateValidatedAsync(id, dto, ct);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await _manager.DeleteAsync(id, ct);

            if (!result.Succeeded)
                return NotFound("No encontrado");

            return Ok();
        }
    }

}
