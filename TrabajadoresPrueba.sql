CREATE DATABASE trabajadoresprueba;
GO

USE trabajadoresprueba;
GO

CREATE TABLE trabajadores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(MAX) NOT NULL,
    Apellidos NVARCHAR(MAX) NOT NULL,
    TipoDocumento NVARCHAR(MAX) NOT NULL,
    NumeroDocumento NVARCHAR(MAX) NOT NULL,
    Sexo NVARCHAR(MAX) NOT NULL,
    FechaNacimiento DATETIME2(6) NOT NULL,
    Direccion NVARCHAR(MAX),
    Foto NVARCHAR(MAX)
);
GO

CREATE PROCEDURE sp_BuscarTrabajadores
    @p_texto NVARCHAR(MAX)
AS
BEGIN
    SELECT *
    FROM trabajadores
    WHERE Nombres LIKE '%' + @p_texto + '%'
       OR Apellidos LIKE '%' + @p_texto + '%'
       OR NumeroDocumento LIKE '%' + @p_texto + '%'
    ORDER BY Apellidos, Nombres;
END;
GO


CREATE PROCEDURE sp_EditarTrabajador
    @p_id INT,
    @p_nombres NVARCHAR(MAX),
    @p_apellidos NVARCHAR(MAX),
    @p_tipoDocumento NVARCHAR(MAX),
    @p_numeroDocumento NVARCHAR(MAX),
    @p_sexo NVARCHAR(MAX),
    @p_fechaNacimiento DATETIME2(6),
    @p_direccion NVARCHAR(MAX),
    @p_foto NVARCHAR(MAX)
AS
BEGIN
    UPDATE trabajadores
    SET Nombres = @p_nombres,
        Apellidos = @p_apellidos,
        TipoDocumento = @p_tipoDocumento,
        NumeroDocumento = @p_numeroDocumento,
        Sexo = @p_sexo,
        FechaNacimiento = @p_fechaNacimiento,
        Direccion = @p_direccion,
        Foto = ISNULL(@p_foto, Foto)
    WHERE Id = @p_id;
END;
GO



CREATE PROCEDURE sp_EliminarTrabajador
    @p_id INT
AS
BEGIN
    DELETE FROM trabajadores
    WHERE Id = @p_id;
END;
GO




CREATE PROCEDURE sp_ListarTrabajadores
    @p_filtroSexo NVARCHAR(MAX) = NULL
AS
BEGIN
    SELECT *
    FROM trabajadores
    WHERE (@p_filtroSexo IS NULL OR Sexo = @p_filtroSexo)
    ORDER BY Apellidos, Nombres;
END;
GO




CREATE PROCEDURE sp_ObtenerTrabajadorPorId
    @p_id INT
AS
BEGIN
    SELECT *
    FROM trabajadores
    WHERE Id = @p_id;
END;
GO



CREATE PROCEDURE sp_RegistrarTrabajador
    @p_nombres NVARCHAR(MAX),
    @p_apellidos NVARCHAR(MAX),
    @p_tipoDocumento NVARCHAR(MAX),
    @p_numeroDocumento NVARCHAR(MAX),
    @p_sexo NVARCHAR(MAX),
    @p_fechaNacimiento DATETIME2(6),
    @p_direccion NVARCHAR(MAX),
    @p_foto NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO trabajadores
        (Nombres, Apellidos, TipoDocumento, NumeroDocumento, Sexo, FechaNacimiento, Direccion, Foto)
    VALUES
        (@p_nombres, @p_apellidos, @p_tipoDocumento, @p_numeroDocumento,
         @p_sexo, @p_fechaNacimiento, @p_direccion, @p_foto);

    SELECT SCOPE_IDENTITY() AS Id;
END;
GO

