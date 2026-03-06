namespace FOOD_CAMPUS.src.Domain;

public class DetallePedido
{
    public int IdDetalle { get; set; }
    public int IdPedido { get; set; }
    public int IdPlatillo { get; set; }
    public decimal Subtotal { get; set; }

    private int _cantidad;
    public int Cantidad
    {
        get => _cantidad;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Cantidad), "La cantidad debe ser mayor a 0.");
            }
            _cantidad = value;
        }
    }
}
