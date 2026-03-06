promp 1:
crea un script SQL para:

crea una conexión con la base de datos "test_utm_abco" (la base de datos ya existe, asegúrate de usarlo)
ubicación de salida: /db/scripts/01_tablas

tabla1:
tabla "restaurante":
Columnas: 'Id', 'Nombre', 'Especialidad', 'HorarioApertura', 'HorarioCierre'
Restricciones: 
- el 'Nombre' no debe estar vacío y no se debe repetir
- el 'Id' no debe repetirse

Incluye SET NOCOUNT ON y SET XACT_ABORT ON al inicio del script.
- Usa comentarios detallados que expliquen la arquitectura de cada tabla.
- IMPORTANTE: Para asegurar que el esquema esté actualizado, si la tabla ya existe, elimínala (DROP TABLE IF EXISTS) antes de volver a crearla.

tabla2:
tabla "Pedido":
Columnas: 'IdPedido', 'IdUsuario', 'IdRestaurante', 'FechaHora', 'CostoEnvio'
Restricciones: 
- El 'CostoEnvio' no debe ser negativo por lo tanto verifica que no sea un numero negativo
- Asegúrate de que 'IdRestaurante' sea una clave foránea que referencie a 'Restaurante.Id'.
- Asegúrate de que DetallesPedido.IdPedido sea una clave foránea que referencie a Pedido.IdPedido.

Incluye SET NOCOUNT ON y SET XACT_ABORT ON al inicio del script.
- Usa comentarios detallados que expliquen la arquitectura de cada tabla.
- IMPORTANTE: Para asegurar que el esquema esté actualizado, si la tabla ya existe, elimínala (DROP TABLE IF EXISTS) antes de volver a crearla.

tabla3:
Tabla: "DetallesPedido"
Columnas: 'IdDetalle', 'IdPedido', 'IdPlatillo', 'Cantidad', 'Subtotal'
Restricciones: el 'IdDetalle' y 'IdPedido' no debe repetirse 

Incluye SET NOCOUNT ON y SET XACT_ABORT ON al inicio del script.
- Usa comentarios detallados que expliquen la arquitectura de cada tabla.
- IMPORTANTE: Para asegurar que el esquema esté actualizado, si la tabla ya existe, elimínala (DROP TABLE IF EXISTS) antes de volver a crearla.

nota: 
- La tabla 'Pedidos' debe tener una capacidad de registro de 20 pedidos 
