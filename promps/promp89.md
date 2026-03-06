Al terminar cada promp notifica al usuario si esta bien o necesecita generar alguna modificación al codigo generado

promp8: 
Ubicación de salida: /src/Application/Interfaces

crea una interfaz llamada "IRestauranteRepository.cs"
en ella implementaremos:
- ObtenerPorNombreAsync(string nombre): Debe devolver la entidad Restaurante. (Nota: La validación de existencia y el mensaje de error se manejarán en el Caso de Uso, no en la interfaz).

- ObtenerTodosAsync(): Debe devolver una lista (IEnumerable) de todas las entidades Restaurante.

- ObtenerPorIdAsync(int id): Método necesario para operaciones de búsqueda exacta.

- AgregarAsync(Restaurante restaurante): Para permitir el registro de nuevos locales.

promp9:
Crea una interfaz llamada IPedidoRepository dentro de la capa de Application.

Debe incluir un método asíncrono llamado "RegistrarPedidoAsync": 

- Este método debe recibir como parámetro un objeto de tipo Pedido (el cual ya contiene su lista de DetallesPedido gracias a la relación Maestro-Detalle definida en el Dominio).

- La interfaz debe estar diseñada para que la implementación posterior sea capaz de registrar la información en las tablas 'Pedido' y 'DetallePedido' de forma atómica.

- Asegúrate de que no contenga ninguna referencia a SQL o Dapper, solo tipos de datos del Dominio." 