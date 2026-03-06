-- Habilitar la terminación de la transacción en caso de error y evitar el recuento de filas afectadas.
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

-- Usar la base de datos especificada.
USE test_utm_abco;
GO

-- ========= Poblar Tabla Restaurante =========
-- Arquitectura de inserción:
-- Este script inserta datos de ejemplo en la tabla 'restaurante'.
-- Para asegurar la idempotencia, primero se eliminan las filas con los IDs que se van a insertar.
-- Esto permite ejecutar el script múltiples veces sin generar errores por duplicados de clave primaria.

-- Eliminar restaurantes existentes con los IDs que se van a insertar para asegurar la idempotencia.
DELETE FROM dbo.restaurante
WHERE Id IN (1, 2, 3, 4, 5, 6, 7);
GO

-- Insertar datos en la tabla 'restaurante'.
INSERT INTO dbo.restaurante (Id, Nombre, Especialidad, HorarioApertura, HorarioCierre)
VALUES
    (1, 'El Sabor Andino', 'Peruana', '12:00:00', '22:00:00'),
    (2, 'La Pizzería Clásica', 'Italiana', '11:00:00', '23:00:00'),
    (3, 'Sushi Master', 'Japonesa', '13:00:00', '22:30:00'),
    (4, 'Burgers & Shakes', 'Americana', '10:00:00', '23:59:00'),
    (5, 'Tacos y Más', 'Mexicana', '12:30:00', '22:00:00'),
    (6, 'Curry House', 'India', '11:30:00', '22:00:00'),
    (7, 'La Pasta Nostra', 'Italiana', '12:00:00', '23:00:00');
GO
