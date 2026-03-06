using System.Linq;

namespace FOOD_CAMPUS.src.Domain;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdUsuario { get; set; }
    public int IdRestaurante { get; set; }
    public DateTime FechaHora { get; set; }
    public decimal CostoEnvio { get; set; }

    // Inicializa la lista de detalles para evitar referencias nulas.
    public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

    // Propiedad calculada que suma el subtotal de todos los detalles y el costo de envío.
    public decimal Total
    {
        get
        {
            return Detalles.Sum(d => d.Subtotal) + CostoEnvio;
        }
    }
}
