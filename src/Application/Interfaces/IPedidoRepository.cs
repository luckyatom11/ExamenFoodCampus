using FOOD_CAMPUS.src.Domain;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Application.Interfaces;

public interface IPedidoRepository
{
    Task RegistrarPedidoAsync(Pedido pedido);
    Task<Pedido?> ObtenerPedidoPorIdAsync(int id);
}
