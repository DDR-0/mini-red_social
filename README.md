# Preparar la base de datos local

Los scripts están separados para que la base se cree primero y el esquema se aplique
después. El segundo archivo fue generado desde las migraciones de Entity Framework
Core y registra las migraciones aplicadas en `__EFMigrationsHistory`.

## Crear la base y las tablas

1. Abrir SQL Server Management Studio o la extensión **SQL Server (mssql)** de VS Code
   y conectarse a la instancia local con una cuenta que pueda crear bases de datos.
2. Buscar la carpeta Scripts donde estan todos los archivos sql y ejecutar `01_create_database.sql`.
3. Ejecutar `02_schema_migrations.sql`. El script selecciona `MiniRedSocialDb` y crea
   las nueve tablas, claves foráneas e índices de las migraciones actuales. Es
   idempotente respecto a esas migraciones; no borra ni reinicia los datos.
4. Opcionalmente, editar el nombre de cuenta en `03_grant_windows_access.sql` y
   ejecutarlo como administrador de SQL Server para crear un login de Windows con
   permisos de lectura y escritura sobre la base.

Se usa autenticación de Windows y no se incluye ninguna contraseña en el repositorio.
El script de permisos crea el login y usuario para la cuenta indicada; no otorga
permisos de administrador ni permisos para modificar el esquema.

## Conectar la aplicación desde VS Code

Abrir una terminal de PowerShell en esta carpeta (`mini-red social1`) y configurar
la cadena de conexión local como User Secrets:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS;Database=MiniRedSocialDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

Cambiar `localhost\SQLEXPRESS` por el nombre real de la instancia del profesor.
Ejemplos habituales son `localhost` para la instancia predeterminada y
`localhost\MSSQLSERVER03` para una instancia con ese nombre. User Secrets se guarda
fuera del repositorio y reemplaza la conexión local predeterminada de `appsettings.json`
en el entorno Development.

Luego ejecutar:

```powershell
dotnet run --launch-profile https
```

La configuración actual escucha en `https://localhost:7123`. La API y las vistas
MVC usan la misma base de datos. En VS Code se puede inspeccionar la base con la
extensión **SQL Server (mssql)** y probar la API con Postman 
por ejemplo `GET https://localhost:7123/api/recursos`.

Los endpoints de escritura necesitan que la aplicación se conecte con permisos de
lectura y escritura. Para aplicar futuras migraciones desde la aplicación o EF Core,
la cuenta también necesita permisos de DDL; el profesor puede ejecutar las
migraciones con una cuenta administradora en vez de conceder esos permisos a la
cuenta normal de la aplicación.
