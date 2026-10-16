# Preparar la base de datos local

Los scripts están separados para que la base se cree primero y el esquema se aplique
después. El segundo archivo fue generado desde las migraciones de Entity Framework
Core y registra las migraciones aplicadas en `__EFMigrationsHistory`.

## Crear la base y las tablas

1. Abrir SQL Server Management Studio o la extensión **SQL Server (mssql)** de VS Code
   y conectarse a la instancia local con una cuenta que pueda crear bases de datos.
2. Localizar la carpeta Scripts, donde se encuentran los archivos SQL y 
   ejecutar `01_create_database.sql`.
4. Ejecutar `02_schema_migrations.sql`. El script selecciona `MiniRedSocialDb` y crea
   las nueve tablas, claves foráneas e índices de las migraciones actuales.
5. Opcionalmente, editar el nombre de cuenta en `03_grant_windows_access.sql` y
   ejecutarlo como administrador de SQL Server para crear un login de Windows con
   permisos de lectura y escritura sobre la base.

## Conectar la aplicación desde VS Code

Abrir una terminal de PowerShell en esta carpeta (`mini-red social1`) y configurar
la cadena de conexión local como User Secrets:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS;Database=MiniRedSocialDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

Cambiar `localhost\SQLEXPRESS` por el nombre real de su instancia.
Ejemplos habituales son `localhost` para la instancia predeterminada y
`localhost\MSSQLSERVER03` para una instancia con ese nombre. User Secrets se guarda
fuera del repositorio y reemplaza la conexión local predeterminada de `appsettings.json`
en el entorno Development.

Luego ejecutar:

```powershell
dotnet run --launch-profile https
```

La configuración actual escucha en `http://localhost:5000`. La API y las vistas
MVC usan la misma base de datos. En VS Code se puede inspeccionar la base con la
extensión **SQL Server (mssql)** y probar la API con **REST Client**, Postman o
Insomnia, por ejemplo `GET http://localhost:5000/api/recursos`.

Los endpoints de escritura necesitan que la aplicación se conecte con permisos de
lectura y escritura. Para aplicar futuras migraciones desde la aplicación o EF Core,
la cuenta también necesita permisos de DDL; se pueden ejecutar las
migraciones con una cuenta administradora en vez de conceder esos permisos a la
cuenta normal de la aplicación.

## Instalar y compilar en otro equipo

### Requisitos

- Windows 10/11.
- .NET 9 SDK (`9.0.x`).
- SQL Server local (por ejemplo, Express o Developer).
- VS Code es opcional. Se recomiendan las extensiones **C# Dev Kit** y **SQL Server (mssql)**.
- Postman, Insomnia o la extensión **REST Client** para probar solicitudes que escriben datos.

Verificar el SDK instalado:

```powershell
dotnet --list-sdks
```

Clonar el repositorio y entrar a la carpeta que contiene el archivo `.csproj`:

```powershell
git clone https://github.com/DDR-0/mini-red_social.git
cd ".\mini-red_social\mini-red social1"
```

Restaurar los paquetes NuGet y compilar:

```powershell
dotnet restore ".\mini-red social1.csproj"
dotnet build ".\mini-red social1.csproj"
```

No es necesario instalar las librerías una por una: `dotnet restore` las descarga desde el proyecto. Las dependencias directas actuales son:

| Paquete | Versión |
|---|---:|
| `Aspire.Microsoft.EntityFrameworkCore.SqlServer` | 9.1.0 |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 9.0.0 |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.20 |
| `Microsoft.EntityFrameworkCore.Sqlite` | 9.0.20 |
| `Microsoft.EntityFrameworkCore.SqlServer` | 9.0.20 |
| `Microsoft.EntityFrameworkCore.Tools` | 9.0.20 |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | 9.0.0 |

ASP.NET Core viene incluido con el SDK y no se instala aparte. Después de crear la base de datos con los scripts anteriores, configurar User Secrets desde la carpeta del proyecto y ejecutar:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS;Database=MiniRedSocialDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
dotnet run --launch-profile https
```

Reemplazar `localhost\SQLEXPRESS` por el nombre de la instancia SQL Server del equipo. La aplicación queda disponible en `http://localhost:5000`.

### Herramienta opcional para migraciones

No hace falta instalar EF CLI para ejecutar la aplicación ni para preparar la base usando los scripts SQL incluidos. Solo si se van a crear o administrar migraciones desde la terminal:

```powershell
dotnet tool install --global dotnet-ef --version 9.0.20
```

Si `dotnet-ef` ya está instalado, actualizarlo a esa versión:

```powershell
dotnet tool update --global dotnet-ef --version 9.0.20
```
