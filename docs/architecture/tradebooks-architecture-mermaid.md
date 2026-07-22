# Arquitectura TradeBooks (Fase 1)

## Diagrama Mermaid

```mermaid
flowchart LR
    Browser[Navegador]\nUsuario --> Web[TradeBooks.Web\nASP.NET MVC + Bootstrap]
    Web --> Api[TradeBooks.Api\nASP.NET Core Web API]

    Api --> App[TradeBooks.Application\nServicios + DTOs]
    App --> Domain[TradeBooks.Domain\nEntidades + Reglas básicas]
    App --> Infra[TradeBooks.Infrastructure\nEF Core + SQL Server + LiteDB]

    Infra --> Sql[(SQL Server)]
    Infra --> Lite[(LiteDB\nException Logs)]

    Api --> Auth0[Auth0\nJWT / RBAC]
    Api --> AuditSvc[TradeBooks.AuditService\nDocker]
    AuditSvc --> AuditLite[(LiteDB\nAudit records)]

    subgraph Azure
        Web
        Api
        AuditSvc
        Sql
    end

    subgraph Docker
        AuditSvc
    end
```

## Cómo recrearlo en Draw.io

1. Crear bloques para: Browser, Web, API, Application, Domain, Infrastructure, SQL Server, LiteDB, Auth0, AuditService, Azure y Docker.
2. Dibujar relaciones en este orden: Browser→Web→API→Application→Domain/Infrastructure.
3. Conectar Infrastructure con SQL Server y LiteDB.
4. Conectar API con Auth0 y AuditService.
5. Encerrar Web/API/AuditService/SQL en un contenedor "Azure".
6. Encerrar AuditService en un contenedor "Docker".

## Cómo recrearlo en Enterprise Architect

1. Crear un diagrama de despliegue (Deployment Diagram).
2. Modelar nodos: Browser, Azure, Docker.
3. Dentro de Azure ubicar: TradeBooks.Web, TradeBooks.Api, SQL Server.
4. Dentro de Docker ubicar: TradeBooks.AuditService.
5. Modelar componentes lógicos: Application, Domain, Infrastructure y enlazarlos desde API.
6. Agregar nodos externos Auth0 y LiteDB con conectores de dependencia.
