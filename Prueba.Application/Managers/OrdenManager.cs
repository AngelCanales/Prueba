using AutoMapper;
using Prueba.Application.DTOs;
using Prueba.Application.Interfaces;
using Prueba.Application.Results;
using Prueba.Domain.Entities;

namespace Prueba.Application.Managers
{
    public class OrdenManager : GenericManager<Orden, OrdenDto, long>, IOrdenManager
    {
        public OrdenManager(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public async Task<OperationResult<OrdenDto>> CreateValidatedAsync(
     OrdenCreateDto dto, CancellationToken ct)
        {
            var clienteRepo = _uow.Repository<Cliente, long>();
            var cliente = await clienteRepo.GetByIdAsync(dto.ClienteId);
            if (cliente == null)
                return OperationResult<OrdenDto>.Failure(
                    "Error al crear la orden",
                    new[] { "El cliente especificado no existe" }
                );

            await _uow.BeginTransactionAsync(ct);

            try
            {
                var orden = new Orden
                {
                    ClienteId = dto.ClienteId,
                    Impuesto = 0,
                    Subtotal = 0,
                    Total = 0,
                };

                await _uow.Repository<Orden, long>().AddAsync(orden);

                decimal subtotalTotal = 0;
                decimal impuestoTotal = 0;

                var productoRepo = _uow.Repository<Producto, long>();
                var detalleRepo = _uow.Repository<DetalleOrden, long>();

                foreach (var detDto in dto.Detalle)
                {
                    var producto = await productoRepo.GetByIdAsync(detDto.ProductoId);
                    if (producto == null)
                        return OperationResult<OrdenDto>.Failure(
                            "Error al procesar la orden",
                            new[] { $"El producto con ID {detDto.ProductoId} no existe" }
                        );

                    if (producto.Existencia < detDto.Cantidad)
                        return OperationResult<OrdenDto>.Failure(
                            "Error al procesar la orden",
                            new[] {
                        $"El producto '{producto.Nombre}' no tiene suficientes existencias. " +
                        $"Disponible: {producto.Existencia}, Solicitado: {detDto.Cantidad}"
                            }
                        );

                    decimal subtotal = detDto.Cantidad * producto.Precio;
                    decimal impuesto = subtotal * 0.15m;
                    decimal total = subtotal + impuesto;

                    var detalle = new DetalleOrden
                    {
                        Orden = orden,
                        ProductoId = producto.ProductoId,
                        Cantidad = detDto.Cantidad,
                        Subtotal = subtotal,
                        Impuesto = impuesto,
                        Total = total
                    };

                    await detalleRepo.AddAsync(detalle);

                    producto.Existencia -= detDto.Cantidad;
                    productoRepo.Update(producto);

                    subtotalTotal += subtotal;
                    impuestoTotal += impuesto;
                }

                orden.Subtotal = subtotalTotal;
                orden.Impuesto = impuestoTotal;
                orden.Total = subtotalTotal + impuestoTotal;

                //_uow.Repository<Orden, long>().Update(orden);

                await _uow.SaveAsync();
                await _uow.CommitTransactionAsync(ct);

                var resultDto = _mapper.Map<OrdenDto>(orden);
                return OperationResult<OrdenDto>.Success(resultDto, "Orden creada exitosamente");
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync(ct);

                return OperationResult<OrdenDto>.Failure(
                    "Error al crear la orden",
                    new[] { ex.Message }
                );
            }
        }

        public async Task<OperationResult<OrdenDto>> UpdateValidatedAsync(
        int id, OrdenUpdateDto dto, CancellationToken ct)
        {
            var repo = _uow.Repository<Cliente, long>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return OperationResult<OrdenDto>.Failure("Orden no encontrado");

           
            _mapper.Map(dto, entity);
            repo.Update(entity);
            await _uow.SaveAsync();

            var resultDto = _mapper.Map<OrdenDto>(entity);
            return OperationResult<OrdenDto>.Success(resultDto, "Orden actualizado");
        }
    }
}
