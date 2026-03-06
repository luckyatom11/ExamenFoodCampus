using FOOD_CAMPUS.src.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Application.Interfaces;

public interface IRestauranteRepository
{
    Task<Restaurante?> ObtenerPorNombreAsync(string nombre);
    Task<IEnumerable<Restaurante>> ObtenerTodosAsync();
    Task<Restaurante?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Restaurante restaurante);
}
