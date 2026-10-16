-- Generated from Entity Framework Core migrations.
-- Run after 01_create_database.sql.
USE [MiniRedSocialDb];
GO

﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [Recursos] (
        [recurso_id] int NOT NULL IDENTITY,
        [nombre] nvarchar(max) NOT NULL,
        [type] int NOT NULL,
        [capacidad] int NOT NULL,
        [creado_en] datetime2 NOT NULL,
        [modificado_en] datetime2 NOT NULL,
        CONSTRAINT [PK_Recursos] PRIMARY KEY ([recurso_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [Roles] (
        [roles_id] int NOT NULL IDENTITY,
        [nombre] nvarchar(max) NOT NULL,
        [descripcion] nvarchar(max) NULL,
        [activo] bit NOT NULL,
        [creado_en] datetime2 NOT NULL,
        [modificado_en] datetime2 NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([roles_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [SnapshotsDiarios] (
        [snapshot_id] int NOT NULL IDENTITY,
        [fecha] datetime2 NOT NULL,
        [usuarios_activos_totales] int NOT NULL,
        [interacciones_totales_creadas] int NOT NULL,
        [interacciones_totales_resueltas] int NOT NULL,
        [frecuencia_uso_recursos_usados] decimal(9,6) NOT NULL,
        [metadatos] nvarchar(max) NOT NULL,
        [creado_en] datetime2 NOT NULL,
        CONSTRAINT [PK_SnapshotsDiarios] PRIMARY KEY ([snapshot_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [TiposInteraccion] (
        [tipo_interaccion_id] int NOT NULL IDENTITY,
        [nombre] nvarchar(max) NOT NULL,
        [descripcion] nvarchar(max) NULL,
        [requiere_recurso] bit NOT NULL,
        [esta_activo] bit NOT NULL,
        [creado_en] datetime2 NOT NULL,
        [modificado_en] datetime2 NOT NULL,
        [borrado_en] datetime2 NULL,
        CONSTRAINT [PK_TiposInteraccion] PRIMARY KEY ([tipo_interaccion_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [user_id] int NOT NULL IDENTITY,
        [rol_id] int NOT NULL,
        [nombre] nvarchar(max) NOT NULL,
        [apellido] nvarchar(max) NULL,
        [correo] nvarchar(450) NOT NULL,
        [descripcion] nvarchar(max) NULL,
        [contrasena_hash] nvarchar(max) NOT NULL,
        [estado_usuario] int NOT NULL,
        [visiblity] int NOT NULL,
        [creado_en] datetime2 NOT NULL,
        [modificado_en] datetime2 NOT NULL,
        [estado_cuenta] int NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([user_id]),
        CONSTRAINT [FK_Usuarios_Roles_rol_id] FOREIGN KEY ([rol_id]) REFERENCES [Roles] ([roles_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [Interacciones] (
        [interacciones_id] int NOT NULL IDENTITY,
        [tipo_interaccion_id] int NOT NULL,
        [id_usuario] int NOT NULL,
        [titulo] nvarchar(150) NOT NULL,
        [descripcion] nvarchar(2000) NULL,
        [EstadoActual] int NOT NULL,
        [NivelPrivacidad] int NOT NULL,
        [horario_inicio] datetime2 NULL,
        [horario_fin] datetime2 NULL,
        [creado_en] datetime2 NOT NULL,
        [modificado_en] datetime2 NOT NULL,
        [borrado_en] datetime2 NULL,
        CONSTRAINT [PK_Interacciones] PRIMARY KEY ([interacciones_id]),
        CONSTRAINT [FK_Interacciones_TiposInteraccion_tipo_interaccion_id] FOREIGN KEY ([tipo_interaccion_id]) REFERENCES [TiposInteraccion] ([tipo_interaccion_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Interacciones_Usuarios_id_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [Usuarios] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [RegistrosAuditoria] (
        [auditlog_id] int NOT NULL IDENTITY,
        [id_usuario] int NOT NULL,
        [accion] nvarchar(max) NOT NULL,
        [entidad_afectada] nvarchar(max) NOT NULL,
        [id_entidad] nvarchar(max) NOT NULL,
        [direccion_ip] nvarchar(max) NULL,
        [usuario_agente] nvarchar(max) NULL,
        [payload_antes] nvarchar(max) NULL,
        [payload_despues] nvarchar(max) NULL,
        [creado_en] datetime2 NOT NULL,
        CONSTRAINT [PK_RegistrosAuditoria] PRIMARY KEY ([auditlog_id]),
        CONSTRAINT [FK_RegistrosAuditoria_Usuarios_id_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [Usuarios] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [HistorialInteracciones] (
        [historial_id] int NOT NULL IDENTITY,
        [interaccion_id] int NOT NULL,
        [cambio_por_usuario] int NOT NULL,
        [estado_previo] nvarchar(max) NOT NULL,
        [estado_nuevo] nvarchar(max) NOT NULL,
        [motivo] nvarchar(max) NOT NULL,
        [creado_en] datetime2 NOT NULL,
        CONSTRAINT [PK_HistorialInteracciones] PRIMARY KEY ([historial_id]),
        CONSTRAINT [FK_HistorialInteracciones_Interacciones_interaccion_id] FOREIGN KEY ([interaccion_id]) REFERENCES [Interacciones] ([interacciones_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_HistorialInteracciones_Usuarios_cambio_por_usuario] FOREIGN KEY ([cambio_por_usuario]) REFERENCES [Usuarios] ([user_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE TABLE [UsosRecurso] (
        [uso_recurso_id] int NOT NULL IDENTITY,
        [interaccion_id] int NOT NULL,
        [recurso_id] int NOT NULL,
        [fecha_inicio] datetime2 NOT NULL,
        [fecha_fin] datetime2 NOT NULL,
        [Estado_recurso] int NOT NULL,
        [fecha_creacion] datetime2 NOT NULL,
        [fecha_eliminacion] datetime2 NULL,
        CONSTRAINT [PK_UsosRecurso] PRIMARY KEY ([uso_recurso_id]),
        CONSTRAINT [FK_UsosRecurso_Interacciones_interaccion_id] FOREIGN KEY ([interaccion_id]) REFERENCES [Interacciones] ([interacciones_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsosRecurso_Recursos_recurso_id] FOREIGN KEY ([recurso_id]) REFERENCES [Recursos] ([recurso_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_HistorialInteracciones_cambio_por_usuario] ON [HistorialInteracciones] ([cambio_por_usuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_HistorialInteracciones_interaccion_id] ON [HistorialInteracciones] ([interaccion_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_Interacciones_id_usuario] ON [Interacciones] ([id_usuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_Interacciones_tipo_interaccion_id] ON [Interacciones] ([tipo_interaccion_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_RegistrosAuditoria_id_usuario] ON [RegistrosAuditoria] ([id_usuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SnapshotsDiarios_fecha] ON [SnapshotsDiarios] ([fecha]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_UsosRecurso_interaccion_id] ON [UsosRecurso] ([interaccion_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_UsosRecurso_recurso_id] ON [UsosRecurso] ([recurso_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Usuarios_correo] ON [Usuarios] ([correo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    CREATE INDEX [IX_Usuarios_rol_id] ON [Usuarios] ([rol_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009141714_Inicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261009141714_Inicial', N'9.0.20');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014154843_PermitirFechasNulas'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UsosRecurso]') AND [c].[name] = N'fecha_fin');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [UsosRecurso] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [UsosRecurso] ALTER COLUMN [fecha_fin] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014154843_PermitirFechasNulas'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TiposInteraccion]') AND [c].[name] = N'modificado_en');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [TiposInteraccion] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [TiposInteraccion] ALTER COLUMN [modificado_en] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014154843_PermitirFechasNulas'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Recursos]') AND [c].[name] = N'modificado_en');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Recursos] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Recursos] ALTER COLUMN [modificado_en] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014154843_PermitirFechasNulas'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Interacciones]') AND [c].[name] = N'modificado_en');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Interacciones] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Interacciones] ALTER COLUMN [modificado_en] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014154843_PermitirFechasNulas'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261014154843_PermitirFechasNulas', N'9.0.20');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014155326_PermitirFechasNulas2'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'modificado_en');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Usuarios] ALTER COLUMN [modificado_en] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014155326_PermitirFechasNulas2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261014155326_PermitirFechasNulas2', N'9.0.20');
END;

COMMIT;
GO

