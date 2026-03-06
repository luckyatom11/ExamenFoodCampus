Al terminar cada promp notifica al usuario si esta bien o necesecita generar alguna modificación al codigo generado

promp10:
Crea la clase "ConsultarRestaurantesUseCase":
- Debe recibir por inyección de dependencias el 'IRestauranteRepository'.
- Implementa un método EjecutarAsync() que obtenga todos los restaurantes.
nota:
Por cada restaurante, utiliza el Extension Member 'EstaAbierto' para añadir una nota al nombre que diga '(ABIERTO)' o '(CERRADO)' antes de enviarlo a la presentación 

promp 11: 
Crea la clase "RegistrarPedidoUseCase":
- Debe recibir el IPedidoRepository y el IRestauranteRepository.
notas:
- Antes de registrar el pedido, debe buscar el restaurante por su ID y verificar si EstaAbierto.
- Si el restaurante está cerrado, debe lanzar una excepción personalizada que detenga el proceso.
- Si está abierto, debe llamar al método del repositorio para guardar el pedido maestro-detalle."

promp12:
Ubicación de salida: /src/Application/Interfaces
Modifica la interfaz 'IPedidoRepository.cs':
- Agrega la firma del método: `Task<Pedido?> ObtenerPedidoPorIdAsync(int id);`

Ubicación de salida: /src/Infraestructure/Repositories
Modifica la clase 'PedidoRepository.cs':
- Implementa el método `ObtenerPedidoPorIdAsync`.
- Usa Dapper para realizar una consulta que obtenga la información del 'Pedido' y sus 'DetallesPedido' asociados.
- Puedes usar `QueryMultiple` o un `JOIN`. Recuerda mapear correctamente la relación uno a muchos (un Pedido tiene muchos Detalles).

Ubicación de salida: /src/Application/UseCases
Crea la clase 'SeguimientoPedidoUseCase.cs':
- Debe recibir por inyección de dependencias el 'IPedidoRepository'.
- Implementa un método `EjecutarAsync(int idPedido)` que devuelva un objeto `Pedido`.
- Si el pedido no existe, puede devolver null o lanzar una excepción, según prefieras.
