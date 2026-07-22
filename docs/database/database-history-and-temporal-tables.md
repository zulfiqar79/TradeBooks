# Historial de modelo de base de datos

## Enfoque elegido

TradeBooks usa **Entity Framework Core Migrations** para versionar el esquema SQL Server.

También se configuraron tablas temporales de SQL Server para entidades críticas:

- Users
- Books
- Exchanges
- Subscriptions
- SubscriptionPayments

## Cómo crear una migración

Desde la raíz del repositorio:

```bash
dotnet ef migrations add <NombreMigracion> --project src/TradeBooks.Infrastructure --startup-project src/TradeBooks.Api
```

## Cómo aplicar migraciones

```bash
dotnet ef database update --project src/TradeBooks.Infrastructure --startup-project src/TradeBooks.Api
```

## Cómo consultar historial temporal

Ejemplo SQL (Books):

```sql
SELECT *
FROM [Books]
FOR SYSTEM_TIME ALL
WHERE [Id] = 1
ORDER BY [PeriodStart];
```

## Respaldo y restauración

1. Programar backup diario de Azure SQL Database.
2. Verificar restore en entorno de prueba.
3. Restaurar a punto en el tiempo (Point-In-Time Restore) cuando sea necesario.
