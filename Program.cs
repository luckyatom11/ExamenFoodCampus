using FOOD_CAMPUS.src.Application.Interfaces;
using FOOD_CAMPUS.src.Application.UseCases;
using FOOD_CAMPUS.src.Domain;
using FOOD_CAMPUS.src.Infrastructure.Data;
using FOOD_CAMPUS.src.Infrastructure.Repositories; // Explicitly add this using directive
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Dapper; // Add this for SqlMapper

// Register custom Dapper type handlers
SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

// Configuración del Host para Inyección de Dependencias
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Registrar el contexto de Dapper
        services.AddSingleton<DapperContext>();

        // Registrar repositorios
        services.AddScoped<IRestauranteRepository, RestauranteRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        // Registrar Casos de Uso
        services.AddScoped<ConsultarRestaurantesUseCase>();
        services.AddScoped<RegistrarPedidoUseCase>();
        services.AddScoped<SeguimientoPedidoUseCase>();
    })
    .Build();

// Obtener los servicios necesarios
using var serviceScope = host.Services.CreateScope();
var services = serviceScope.ServiceProvider;

var consultarRestaurantesUseCase = services.GetRequiredService<ConsultarRestaurantesUseCase>();
var restauranteRepository = services.GetRequiredService<IRestauranteRepository>(); // Usando el repositorio directamente para registrar por simplicidad.
var registrarPedidoUseCase = services.GetRequiredService<RegistrarPedidoUseCase>();
var seguimientoPedidoUseCase = services.GetRequiredService<SeguimientoPedidoUseCase>();

// Bucle principal del programa
bool running = true;
while (running)
{
    Console.WriteLine("--- Menú Principal ---");
    Console.WriteLine("1. Consultar Restaurantes");
    Console.WriteLine("2. Registrar Restaurante");
    Console.WriteLine("3. Realizar Pedido");
    Console.WriteLine("4. Seguimiento de Pedido");
    Console.WriteLine("5. Salir");
    Console.Write("Seleccione una opción: ");

    string? input = Console.ReadLine();
    if (int.TryParse(input, out int option))
    {
        switch (option)
        {
            case 1:
                Console.WriteLine("--- Restaurantes Disponibles ---");
                var restaurantes = await consultarRestaurantesUseCase.EjecutarAsync();
                foreach (var r in restaurantes)
                {
                    Console.WriteLine($"ID: {r.Id}, Nombre: {r.NombreMostrado}, Especialidad: {r.Especialidad}, Horario: {r.HorarioApertura} - {r.HorarioCierre}");
                }
                break;
            case 2:
                Console.WriteLine("--- Registrar Nuevo Restaurante ---");
                Console.Write("ID del Restaurante: ");
                if (!int.TryParse(Console.ReadLine(), out int newRestauranteId))
                {
                    Console.WriteLine("ID inválido.");
                    break;
                }
                Console.Write("Nombre del Restaurante: ");
                string? newRestauranteNombre = Console.ReadLine();
                Console.Write("Especialidad: ");
                string? newRestauranteEspecialidad = Console.ReadLine();
                Console.Write("Horario de Apertura (HH:MM): ");
                if (!TimeOnly.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out TimeOnly newHorarioApertura))
                {
                    Console.WriteLine("Horario de apertura inválido.");
                    break;
                }
                Console.Write("Horario de Cierre (HH:MM): ");
                if (!TimeOnly.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out TimeOnly newHorarioCierre))
                {
                    Console.WriteLine("Horario de cierre inválido.");
                    break;
                }

                try
                {
                    await restauranteRepository.AgregarAsync(new Restaurante
                    {
                        Id = newRestauranteId,
                        Nombre = newRestauranteNombre ?? "Nombre Desconocido",
                        Especialidad = newRestauranteEspecialidad ?? "General",
                        HorarioApertura = newHorarioApertura,
                        HorarioCierre = newHorarioCierre
                    });
                    Console.WriteLine("Restaurante registrado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar restaurante: {ex.Message}");
                }
                break;
            case 3:
                Console.WriteLine("--- Realizar Nuevo Pedido ---");
                Console.Write("ID del Restaurante para el pedido: ");
                if (!int.TryParse(Console.ReadLine(), out int pedidoRestauranteId))
                {
                    Console.WriteLine("ID de restaurante inválido.");
                    break;
                }
                Console.Write("ID de Usuario: ");
                if (!int.TryParse(Console.ReadLine(), out int pedidoUsuarioId))
                {
                    Console.WriteLine("ID de usuario inválido.");
                    break;
                }
                Console.Write("Costo de Envío: ");
                if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal costoEnvio))
                {
                    Console.WriteLine("Costo de envío inválido.");
                    break;
                }

                var nuevoPedido = new Pedido
                {
                    IdPedido = new Random().Next(1000, 9999), // Simple ID generation for example
                    IdUsuario = pedidoUsuarioId,
                    IdRestaurante = pedidoRestauranteId,
                    FechaHora = DateTime.Now,
                    CostoEnvio = costoEnvio
                };

                Console.WriteLine("Agregue detalles del pedido (Platillo ID, Cantidad, Subtotal por item). Escriba 'fin' para terminar.");
                int detalleId = 1;
                while (true)
                {
                    Console.Write($"Detalle {detalleId} - ID Platillo: ");
                    string? platilloIdInput = Console.ReadLine();
                    if (platilloIdInput?.ToLower() == "fin") break;
                    if (!int.TryParse(platilloIdInput, out int idPlatillo))
                    {
                        Console.WriteLine("ID de platillo inválido. Intente de nuevo o escriba 'fin'.");
                        continue;
                    }

                    Console.Write($"Detalle {detalleId} - Cantidad: ");
                    if (!int.TryParse(Console.ReadLine(), out int cantidad))
                    {
                        Console.WriteLine("Cantidad inválida. Intente de nuevo.");
                        continue;
                    }
                    Console.Write($"Detalle {detalleId} - Subtotal por Platillo: ");
                    if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal subtotal))
                    {
                        Console.WriteLine("Subtotal inválido. Intente de nuevo.");
                        continue;
                    }

                    nuevoPedido.Detalles.Add(new DetallePedido
                    {
                        IdDetalle = detalleId++,
                        IdPlatillo = idPlatillo,
                        Cantidad = cantidad,
                        Subtotal = subtotal
                    });
                }

                try
                {
                    await registrarPedidoUseCase.EjecutarAsync(nuevoPedido);
                    Console.WriteLine($"Pedido {nuevoPedido.IdPedido} registrado exitosamente. Total: {nuevoPedido.Total}");
                }
                catch (RestauranteCerradoException ex)
                {
                    Console.WriteLine($"Error al realizar pedido: {ex.Message}");
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"Error al realizar pedido: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado al registrar pedido: {ex.Message}");
                }
                break;
            case 4:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--- Seguimiento de Pedido ---");
                Console.Write("Ingrese el ID del Pedido: ");
                if (!int.TryParse(Console.ReadLine(), out int pedidoIdSeguimiento))
                {
                    Console.WriteLine("ID de pedido inválido.");
                    Console.ResetColor();
                    break;
                }

                var pedidoEncontrado = await seguimientoPedidoUseCase.EjecutarAsync(pedidoIdSeguimiento);
                if (pedidoEncontrado != null)
                {
                    Console.WriteLine($"Detalles del Pedido ID: {pedidoEncontrado.IdPedido}");
                    Console.WriteLine($"  Usuario ID: {pedidoEncontrado.IdUsuario}");
                    Console.WriteLine($"  Restaurante ID: {pedidoEncontrado.IdRestaurante}");
                    Console.WriteLine($"  Fecha/Hora: {pedidoEncontrado.FechaHora}");
                    Console.WriteLine($"  Costo de Envío: {pedidoEncontrado.CostoEnvio:C}");
                    Console.WriteLine($"  Total del Pedido: {pedidoEncontrado.Total:C}");
                    Console.WriteLine("  --- Platillos ---");
                    if (pedidoEncontrado.Detalles.Any())
                    {
                        foreach (var detalle in pedidoEncontrado.Detalles)
                        {
                            Console.WriteLine($"    Detalle ID: {detalle.IdDetalle}, Platillo ID: {detalle.IdPlatillo}, Cantidad: {detalle.Cantidad}, Subtotal: {detalle.Subtotal:C}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("    No hay platillos en este pedido.");
                    }
                }
                else
                {
                    Console.WriteLine($"Pedido con ID {pedidoIdSeguimiento} no encontrado.");
                }
                Console.ResetColor();
                break;
            case 5:
                running = false;
                Console.WriteLine("Saliendo del programa.");
                break;
            default:
                Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
    }
}
