# Matriz de cumplimiento - Fase 1

| Categoría | Fuente PDF | Estado Fase 1 | Evidencia |
|---|---|---|---|
| Requerimientos funcionales RF-001..RF-040 | Sección 10.2.1 | Parcial | Estructura y base de API creada |
| Requerimientos no funcionales RNF-001..RNF-030 | Sección 10.2.3 | Parcial | Capas, Swagger, middleware, health |
| Entidades principales | Diagrama de clases 10.6.6 | Parcial | `TradeBooks.Domain/Entities/*` |
| Casos de uso CU-001..CU-023 | Índice 10.6.2 + especificaciones | Parcial | Endpoint base `/api/books`, capas preparadas |
| Reglas de estados (Libro/Intercambio/Suscripción/Pago) | CU-005..CU-017 + glosario | Parcial | Enums y validaciones básicas de transición |
| Arquitectura web por capas | 10.4.5 + 10.6.8 | Cumplido en Fase 1 | `docs/architecture/tradebooks-architecture-mermaid.md` |
| Historial de BD y temporal tables | Requisitos universitarios + SQL Server | Cumplido en Fase 1 | EF Core + migración inicial + doc BD |

## Requisitos extraídos del PDF (resumen)

### Funcionales (resumen)
- Registro/autenticación/perfil
- Publicar/modificar/eliminar libros
- Buscar y reservar
- Intercambios (solicitar/aprobar/rechazar/cancelar/finalizar)
- Suscripción y pagos
- Panel administrador, bitácora y reportes

### No funcionales (resumen)
- Seguridad (TLS, RBAC, protección web)
- Arquitectura desacoplada
- Trazabilidad/auditoría
- Rendimiento objetivo y mantenibilidad
- Portabilidad en Azure

### Entidades detectadas
- Usuario, Libro, Reserva, Intercambio, Suscripción, PagoSuscripción
- HistorialActividad, Notificación, Bitácora (modeladas como base o pendientes de profundidad)

### Reglas de negocio base detectadas
- Libro inicia en `Disponible`
- Intercambio usa ciclo `Solicitado -> Asignado -> Finalizado` (con cancelación/rechazo)
- Suscripción en ciclo `Pendiente/Activa/Vencida/Cancelada/Rechazada`
- Pago en ciclo `Pendiente/Aprobado/Rechazado/Anulado`
