using FOOD_CAMPUS.src.Application.Interfaces;
using FOOD_CAMPUS.src.Domain;
using System;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Application.UseCases;

public class RestauranteCerradoException : Exception
{
    public RestauranteCerradoException(string message) : base(message) { }
}

public class RegistrarPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IRestauranteRepository _restauranteRepository;

    public RegistrarPedidoUseCase(IPedidoRepository pedidoRepository, IRestauranteRepository restauranteRepository)
    {
        _pedidoRepository = pedidoRepository;
        _restauranteRepository = restauranteRepository;
    }

    public async Task EjecutarAsync(Pedido pedido)
    {
        var restaurante = await _restauranteRepository.ObtenerPorIdAsync(pedido.IdRestaurante);

        if (restaurante == null)
        {
            throw new KeyNotFoundException($"El restaurante con ID {pedido.IdRestaurante} no fue encontrado.");
        }

        if (!restaurante.EstaAbierto)
        {
            throw new RestauranteCerradoException($"El restaurante '{restaurante.Nombre}' está cerrado en este momento.");
        }

        await _pedidoRepository.RegistrarPedidoAsync(pedido);
    }
}
