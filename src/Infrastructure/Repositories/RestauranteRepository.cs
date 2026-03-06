using Dapper;
using FOOD_CAMPUS.src.Application.Interfaces;
using FOOD_CAMPUS.src.Domain;
using FOOD_CAMPUS.src.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Infrastructure.Repositories;

public class RestauranteRepository : IRestauranteRepository
{
    private readonly DapperContext _context;

    public RestauranteRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Restaurante?> ObtenerPorNombreAsync(string nombre)
    {
        var sql = "SELECT * FROM dbo.restaurante WHERE Nombre = @Nombre";
        using var connection = _context.CrearConexion();
        return await connection.QuerySingleOrDefaultAsync<Restaurante>(sql, new { Nombre = nombre });
    }

    public async Task<IEnumerable<Restaurante>> ObtenerTodosAsync()
    {
        var sql = "SELECT * FROM dbo.restaurante";
        using var connection = _context.CrearConexion();
        return await connection.QueryAsync<Restaurante>(sql);
    }

    public async Task<Restaurante?> ObtenerPorIdAsync(int id)
    {
        var sql = "SELECT * FROM dbo.restaurante WHERE Id = @Id";
        using var connection = _context.CrearConexion();
        return await connection.QuerySingleOrDefaultAsync<Restaurante>(sql, new { Id = id });
    }

    public async Task AgregarAsync(Restaurante restaurante)
    {
        var sql = @"
            INSERT INTO dbo.restaurante (Id, Nombre, Especialidad, HorarioApertura, HorarioCierre)
            VALUES (@Id, @Nombre, @Especialidad, @HorarioApertura, @HorarioCierre);";
        using var connection = _context.CrearConexion();
        await connection.ExecuteAsync(sql, restaurante);
    }
}
