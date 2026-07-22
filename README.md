# TradeBooks

TradeBooks es un proyecto académico de Ingeniería en Sistemas para gestionar intercambio de libros bajo suscripción.

## Estado actual

Implementación de **Fase 1** completada:

- Estructura de solución por capas
- Entidades y enums base del dominio
- API base con Swagger
- Middleware global de excepciones con respuesta segura
- Health checks básicos
- EF Core con SQL Server
- Migración inicial y tablas temporales en entidades críticas
- Documentación de arquitectura y base de datos

## Estructura

```text
TradeBooks.sln
src/
  TradeBooks.Domain
  TradeBooks.Application
  TradeBooks.Infrastructure
  TradeBooks.Api
  TradeBooks.Web
  TradeBooks.AuditService
tests/
  TradeBooks.Tests
docs/
  architecture/
  database/
  deployment/
```

## Requisitos técnicos

- .NET 8 SDK
- SQL Server

## Configuración

1. Copiar `src/TradeBooks.Api/appsettings.example.json` a `appsettings.Development.json` y completar valores.
2. Configurar Auth0 (Authority/Audience).
3. Configurar cadena de conexión SQL Server.

## Comandos útiles

### Restaurar y compilar

```bash
dotnet restore
dotnet build TradeBooks.sln
```

### Ejecutar API

```bash
dotnet run --project src/TradeBooks.Api
```

### Ejecutar Web

```bash
dotnet run --project src/TradeBooks.Web
```

### Ejecutar AuditService

```bash
dotnet run --project src/TradeBooks.AuditService
```

### Migraciones

```bash
dotnet ef migrations add <NombreMigracion> --project src/TradeBooks.Infrastructure --startup-project src/TradeBooks.Api
dotnet ef database update --project src/TradeBooks.Infrastructure --startup-project src/TradeBooks.Api
```

## Seguridad

- No incluir secretos reales en repositorio.
- Usar variables de entorno o secretos locales.
- Se proveen plantillas: `.env.example`, `appsettings.example.json`.

## Pendiente para próximas fases

- Fase 2: autenticación completa y casos de uso núcleo
- Fase 3: suscripciones/pagos/logs completos
- Fase 4: panel admin y Docker completo
- Fase 5: pruebas finales y documentación de despliegue Azure/SSL
