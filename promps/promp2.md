promp2:
contexto: poblar la tabla 'Restaurante'
requisitos:
Esquema de referencia:

    CREATE TABLE dbo.restaurante (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Especialidad VARCHAR(100),
    HorarioApertura TIME,
    HorarioCierre TIME
);


ejecución:
crea un scrip SQL para poblar la tabla 'Restaurante' con un máximo de 7 restaurantes, los nombres deben ser coherentes y cada uno debe tener una especialidad que se pueden repetir

notas:
-Usa comentarios detallados que expliquen la arquitectura de cada tabla.
- Asegura que el script sea idempotente (puedas ejecutarlo varias veces sin errores).