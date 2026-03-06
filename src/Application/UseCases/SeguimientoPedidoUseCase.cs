using FOOD_CAMPUS.src.Application.Interfaces;
using FOOD_CAMPUS.src.Domain;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Application.UseCases;

public class SeguimientoPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository;

    public SeguimientoPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Pedido?> EjecutarAsync(int idPedido)
    {
        var pedido = await _pedidoRepository.ObtenerPedidoPorIdAsync(idPedido);
        // La lógica de negocio (si el pedido no se encuentra) se puede manejar aquí o en la capa de presentación.
        // Por ahora, simplemente devolvemos el resultado del repositorio.
        return pedido;
    }
}
