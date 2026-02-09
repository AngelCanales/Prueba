using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Application.Results;

namespace Prueba.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {

        private readonly IOrdenManager _manager;
        private readonly ILogger<OrdenesController> _logger;

        public OrdenesController(IOrdenManager manager, ILogger<OrdenesController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            _logger.LogInformation("Obteniendo Orden");
            var result = await _manager.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _manager.GetByIdAsync(id, ct);

            if (!result.Succeeded)
            {
                var error = OperationResult<OrdenDto>.Failure("Error al crear la Orden", ["No existe un Orden con el ID especificado"]);
                return NotFound(error);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] OrdenCreateDto dto, CancellationToken ct)
        {
            if (dto.OrdenId != 0)
                return BadRequest("orden id debe ser 0");

            var result = await _manager.CreateValidatedAsync(dto, ct);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id, [FromBody] OrdenUpdateDto dto, CancellationToken ct)
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

