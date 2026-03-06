using Dapper;
using FOOD_CAMPUS.src.Application.Interfaces;
using FOOD_CAMPUS.src.Domain;
using FOOD_CAMPUS.src.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FOOD_CAMPUS.src.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly DapperContext _context;

    public PedidoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObtenerPedidoPorIdAsync(int id)
    {
        using var connection = _context.CrearConexion();
        var sql = @"
            SELECT * FROM dbo.Pedido WHERE IdPedido = @id;
            SELECT * FROM dbo.DetallesPedido WHERE IdPedido = @id;";

        using (var multi = await connection.QueryMultipleAsync(sql, new { id }))
        {
            var pedido = await multi.ReadSingleOrDefaultAsync<Pedido>();
            if (pedido != null)
            {
                pedido.Detalles = (await multi.ReadAsync<DetallePedido>()).ToList();
            }
            return pedido;
        }
    }

    public async Task RegistrarPedidoAsync(Pedido pedido)
    {
        using var connection = _context.CrearConexion();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            var pedidoSql = @"
                INSERT INTO dbo.Pedido (IdPedido, IdUsuario, IdRestaurante, FechaHora, CostoEnvio)
                VALUES (@IdPedido, @IdUsuario, @IdRestaurante, @FechaHora, @CostoEnvio);";
            
            await connection.ExecuteAsync(pedidoSql, pedido, transaction);

            var detalleSql = @"
                INSERT INTO dbo.DetallesPedido (IdDetalle, IdPedido, IdPlatillo, Cantidad, Subtotal)
                VALUES (@IdDetalle, @IdPedido, @IdPlatillo, @Cantidad, @Subtotal);";

            foreach (var detalle in pedido.Detalles)
            {
                // Asegurarse de que el IdPedido del detalle es el mismo que el del pedido maestro.
                detalle.IdPedido = pedido.IdPedido;
                await connection.ExecuteAsync(detalleSql, detalle, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
