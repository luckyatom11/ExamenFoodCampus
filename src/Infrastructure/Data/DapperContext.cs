using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace FOOD_CAMPUS.src.Infrastructure.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SomeeConnection") 
            ?? throw new ArgumentNullException("Connection string 'SomeeConnection' not found.");
    }

    public IDbConnection CrearConexion()
    {
        return new SqlConnection(_connectionString);
    }
}
