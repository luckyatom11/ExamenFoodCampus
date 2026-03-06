-- Habilitar la terminación de la transacción en caso de error y evitar el recuento de filas afectadas.
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

-- Usar la base de datos especificada.
USE test_utm_abco;
GO

-- ========= Tabla Restaurante =========
-- Arquitectura:
-- Esta tabla almacena la información básica de los restaurantes.
-- 'Id': Es la clave primaria única para cada restaurante.
-- 'Nombre': El nombre del restaurante, no puede ser nulo y debe ser único.
-- 'Especialidad': El tipo de comida que ofrece el restaurante.
-- 'HorarioApertura', 'HorarioCierre': Definen el horario de operación del restaurante.

-- Eliminar la tabla si ya existe para asegurar un esquema limpio y actualizado.
IF OBJECT_ID('dbo.restaurante', 'U') IS NOT NULL
    DROP TABLE dbo.restaurante;
GO

CREATE TABLE dbo.restaurante (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Especialidad VARCHAR(100),
    HorarioApertura TIME,
    HorarioCierre TIME
);
GO

-- ========= Tabla Pedido =========
-- Arquitectura:
-- Esta tabla registra cada pedido realizado por un usuario a un restaurante.
-- 'IdPedido': Clave primaria para identificar unívocamente cada pedido.
-- 'IdUsuario': Identificador del usuario que realiza el pedido (se asume que existe una tabla de usuarios).
-- 'IdRestaurante': Clave foránea que vincula el pedido con el restaurante correspondiente.
-- 'FechaHora': Momento exacto en que se realizó el pedido.
-- 'CostoEnvio': Costo del envío, no puede ser un valor negativo.
-- Nota: Se ha mencionado un límite de 20 pedidos. Esto no es una restricción estándar de tabla,
-- pero podría implementarse con un TRIGGER si fuera un requisito estricto.

-- Eliminar las tablas dependientes y la tabla principal si ya existen.
IF OBJECT_ID('dbo.DetallesPedido', 'U') IS NOT NULL
    DROP TABLE dbo.DetallesPedido;
IF OBJECT_ID('dbo.Pedido', 'U') IS NOT NULL
    DROP TABLE dbo.Pedido;
GO

CREATE TABLE dbo.Pedido (
    IdPedido INT PRIMARY KEY,
    IdUsuario INT, -- Se asume que existirá una tabla de Usuarios
    IdRestaurante INT,
    FechaHora DATETIME,
    CostoEnvio DECIMAL(10, 2),
    CONSTRAINT FK_Pedido_Restaurante FOREIGN KEY (IdRestaurante) REFERENCES dbo.restaurante(Id),
    CONSTRAINT CHK_CostoEnvio_NoNegativo CHECK (CostoEnvio >= 0)
);
GO

-- ========= Tabla DetallesPedido =========
-- Arquitectura:
-- Esta tabla desglosa el contenido de cada pedido, listando los platillos y cantidades.
-- 'IdDetalle': Parte de la clave primaria compuesta, identifica el item del detalle.
-- 'IdPedido': Parte de la clave primaria compuesta y clave foránea que vincula con la tabla Pedido.
-- 'IdPlatillo': Identificador del platillo (se asume que existe una tabla de platillos).
-- 'Cantidad': Número de unidades de un platillo específico.
-- 'Subtotal': Costo total para un item del detalle (Cantidad * Precio del platillo).

-- Eliminar la tabla si ya existe para asegurar un esquema limpio y actualizado.
IF OBJECT_ID('dbo.DetallesPedido', 'U') IS NOT NULL
    DROP TABLE dbo.DetallesPedido;
GO

CREATE TABLE dbo.DetallesPedido (
    IdDetalle INT,
    IdPedido INT,
    IdPlatillo INT, -- Se asume que existirá una tabla de Platillos
    Cantidad INT,
    Subtotal DECIMAL(10, 2),
    CONSTRAINT PK_DetallesPedido PRIMARY KEY (IdDetalle, IdPedido),
    CONSTRAINT FK_DetallesPedido_Pedido FOREIGN KEY (IdPedido) REFERENCES dbo.Pedido(IdPedido)
);
GO
