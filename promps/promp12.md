promp 10:
Configuración: Usa Host.CreateDefaultBuilder para registrar los Repositorios de la capa de Infrastructure y los Casos de Uso de la capa de Application.
modifica el archivo "program.cs" y crea un ciclo while con las siguientes opciones:
1. Consultar Restaurantes (Debe listar los registros de Somee).
2. Registrar Restaurante.
3. Realizar Pedido (Debe pedir ID de restaurante, productos y cantidad).
4. Seguimiento de Pedido.
5. Salir.
Validaciones: Usa int.TryParse para que el programa no se cierre si el usuario ingresa letras en lugar de números.

Para la Opción 4 (Seguimiento de Pedido):
- Solicita al usuario el ID del Pedido (valida que sea numérico).
- Invoca al caso de uso 'SeguimientoPedidoUseCase' (debes inyectarlo previamente).
- Si el pedido existe, muestra sus detalles (ID, Restaurante, Total, etc.) y la lista de platillos.
- IMPORTANTE: Todos los mensajes relacionados con el seguimiento del pedido (títulos, detalles, errores si no se encuentra) deben mostrarse en color ROJO (`Console.ForegroundColor = ConsoleColor.Red`). Recuerda restaurar el color con `Console.ResetColor()` al final.

Nota importante:
Asegúrate de indicar que se deben instalar los siguientes paquetes NuGet para que el proyecto compile y funcione correctamente:
- `dotnet add package Microsoft.Extensions.Hosting`
- `dotnet add package Microsoft.Extensions.Configuration.Json`
- `dotnet add package Dapper`