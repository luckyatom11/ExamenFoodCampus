using FOOD_CAMPUS.src.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Application.UseCases;

// ViewModel para la presentación de datos del restaurante.
public class RestauranteViewModel
{
    public int Id { get; set; }
    public required string NombreMostrado { get; set; }
    public required string Especialidad { get; set; }
    public TimeOnly HorarioApertura { get; set; }
    public TimeOnly HorarioCierre { get; set; }
}

public class ConsultarRestaurantesUseCase
{
    private readonly IRestauranteRepository _restauranteRepository;

    public ConsultarRestaurantesUseCase(IRestauranteRepository restauranteRepository)
    {
        _restauranteRepository = restauranteRepository;
    }

    public async Task<IEnumerable<RestauranteViewModel>> EjecutarAsync()
    {
        var restaurantes = await _restauranteRepository.ObtenerTodosAsync();

        return restaurantes.Select(r => new RestauranteViewModel
        {
            Id = r.Id,
            NombreMostrado = $"{r.Nombre} {(r.EstaAbierto ? "(ABIERTO)" : "(CERRADO)")}",
            Especialidad = r.Especialidad,
            HorarioApertura = r.HorarioApertura,
            HorarioCierre = r.HorarioCierre
        });
    }
}
