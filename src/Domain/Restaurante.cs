namespace FOOD_CAMPUS.src.Domain;

public class Restaurante
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Especialidad { get; set; }
    public TimeOnly HorarioApertura { get; set; }
    public TimeOnly HorarioCierre { get; set; }

    // Propiedad de solo lectura que compara la hora actual para ver si el restaurante está abierto.
    public bool EstaAbierto
    {
        get
        {
            var horaActual = TimeOnly.FromDateTime(DateTime.Now);
            return horaActual >= HorarioApertura && horaActual <= HorarioCierre;
        }
    }
}
