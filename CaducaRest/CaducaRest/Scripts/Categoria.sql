IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categoria] (
    [Id] int NOT NULL IDENTITY,
    [Clave] int NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220918032849_CreacionInicial', N'5.0.14');
GO

COMMIT;
GO

