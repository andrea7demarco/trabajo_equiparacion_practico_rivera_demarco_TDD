# Iteraciones

**Proyecto:** Sistema de Gestión de Agenda  
**Metodología:** Test Driven Development (Red-Green-Refactor)

## Objetivo de este documento
* **Histórico:** Conservar el motivo de cada iteración para un post-análisis.
* **Trazabilidad:** Saber cuántos y cuáles casos de pruebas derivaron de un caso de uso.


### Ciclo RED (Fase de Falla)

| ID CU | Regla de Negocio (Input → Output) | Nombre del Test | Resultado (Expectativa vs Realidad) |
| :--- | :--- | :--- | :--- |
| **CU-01** | Dado cliente y fecha válida, agendar cita y devolver éxito. | `TestAgendarCita()` | **Falló:** Error de compilación (namespaces/clases faltantes). |
| **CU-01** | El turno registrado debe quedar "Pendiente de confirmación". | `TestAgendarCita()` | **Falló:** Lógica incompleta, servicio devuelve objeto vacío. |
| **CU-01** | Si el horario está ocupado, rechazar solicitud. | `TestAgendarCita()` | **Falló:** Lógica incompleta. |
| **CU-02** | Modificar fecha de turno si faltan > 8 horas. | `TestReagendarCita` | **Falló:** Método `reagendarCita` no existe. |
| **CU-02** | Si faltan <= 8 horas, impedir cambio e informar error. | `TestReagendarCita_MenosDe8Horas` | **Falló:** Sin lógica para cálculo de horas. |
| **CU-03** | Dado un turno con estado pendiente, la agenda debe permitir eliminarlo. | `TestEliminarTurnoPendiente` | **Falló:** No existen las clases ni los métodos. |
| **CU-03** | Si un turno se elimina, debe pasar su estado de "Pendiente" a "Cancelado". | `TestEliminarTurnoPendiente` | **Falló:** No existe el estado 'Cancelado'. |
| **CU-03** | Si un turno está confirmado, no puede eliminarse. | `TestEliminarTurnoConfirmado` | **Falló:** No existe el estado 'Confirmado' y lógica incompleta. |
| **CU-03** | Si quedan menos de dos horas para un turno, no puede eliminarse. | `TestEliminarTurnoProximo` | **Falló:** No existe el atributo `Fecha` en el objeto Cita. |
| **CU-03** | Un turno que no pudo eliminarse, no debería cambiar su estado. | `TestEliminarTurnoProximo` | **Pasó (Inesperado):** Posible sobre-diseño previo. |
| **CU-03** | Un turno ya cancelado, no debería poder cancelarse nuevamente. | `TestEliminarTurnoYaCancelado` | **Falló:** Caso no previsto. |
| **CU-03** | Al intentar eliminar una cita con datos no agendados, devolver `false`. | `TestEliminarCitaInexistente` | **Falló:** Método `eliminarCita` no revisaba existencia en lista. |
| **CU-04** | Consultar citas por DNI; si no hay citas programadas, devolver lista vacía. | `TestConsultarCitasUsuarioSinCitas` | **Falló:** Agenda sin lista de citas, sin método de consulta y sin tipo de dato Usuario. |
| **CU-04** | Consultar citas por DNI; si no hay citas para ese usuario, devolver lista vacía. | `TestConsultarCitasUsuarioSinCitas` | **Falló:** Objeto Cita sin referencia al usuario. Luego, método devolvía *todas* las citas. |
| **CU-04** | El usuario debe poder consultar sus citas programadas. | `TestConsultarCitasUsuarioConCitas` | **Pasó (Green):** Cubierto por la lógica de la iteración anterior del CU-03. |
| **CU-04** | Usuario logueado no puede consultar citas de otro usuario. | `TestConsultarCitasOtroUsuario` | **Falló:** Agenda sin atributo de usuario logueado y sin lógica de diferenciación. |
| **CU-04** | Devolver listas vacías ante datos nulos o formato inválido. | `TestConsultarDniInvalidos` | **Falló:** Excepción con DNI nulo. |
| **CU-05** | Confirmar una cita existente dados un DNI y una fecha. | `TestConfirmarCitaProgramada` | **Falló:** No existe método `confirmarCita()` en Agenda. |
| **CU-05** | El estado de la cita confirmada debe pasar a ‘Confirmado’. | `TestConfirmarCitaProgramada` | **Falló:** El método no actualizaba el estado. |
| **CU-05** | Al intentar confirmar cita inexistente, devolver `false`. | `TestConfirmarCitaInexistente` | **Falló:** Excepción al consultar cita inexistente. |
| **CU-05** | Al intentar confirmar cita ya confirmada/cancelada, devolver `false`. | `TestConfirmarCitaNoPendiente` | **Falló:** Método ignoraba el estado previo. |


### Ciclo GREEN (Fase de Implementación)

| ID CU | Expectativa | Qué se implementó (Mínimo Indispensable) | Lógica Extra? | Resultado |
| :--- | :--- | :--- | :--- | :--- |
| **CU-01** | Agendar válido devuelve Éxito, ID y estado Pendiente. | Clase `ServicioAgenda`, método `AgendarCita`, generación GUID, DTOs `SolicitudCita` y `RespuestaCita`. | No. | Pasó |
| **CU-01** | Agendar en horario ocupado devuelve false + mensaje. | Lista privada `List<DateTime>` (BD en memoria) y validación de duplicados. | Sí (BD en memoria necesaria para testear fallos). | Pasó |
| **CU-02** | Reagendar permitido si > 8hs. | Método `reagendarCita`. Cambio de `List` a `Dictionary<Guid, DateTime>` para búsquedas por ID. | Sí (Cambio estructura datos). | Pasó |
| **CU-02** | Reagendar bloqueado si <= 8hs. | Validación de fecha y retorno de respuesta de error. | No. | Pasó |
| **CU-03** | Eliminar turno y devolver true. | Clase `Agenda`, método `eliminarTurno()`, clase `Turno` con enum `Estado`. | No (derivado del test). | Pasó |
| **CU-03** | Turno eliminado cambia a 'Cancelado'. | Estado 'Cancelado' en Enum; cambio de estado en método. | No. | Pasó |
| **CU-03** | Turno confirmado no se elimina. | Estado 'Confirmado' en Enum; validación en `eliminarTurno`. | No. | Pasó |
| **CU-03** | Restricción de 2 horas para eliminar. | Atributo `Fecha` en Cita; validación con `TimeSpan`. | Sí (Guard Clause). | Pasó |
| **CU-03** | Turno cancelado no se cancela de nuevo. | Validación de estado en `eliminarTurno`. | No. | Pasó |
| **CU-04** | Consultar citas devuelve lista vacía si no hay datos. | Atributo lista en `Agenda`, método consulta por DNI, clase `Usuario`. | Encapsulamiento prematuro en Agenda. | Pasó |
| **CU-04** | Lista vacía si usuario no tiene citas. | Filtro de citas por DNI. | No. | Pasó |
| **CU-04** | Bloqueo de consulta de citas ajenas. | Atributo `dniUsuarioLogueado` y validación contra consulta. | No. | Pasó |
| **CU-04** | Manejo de DNIs inválidos. | Validación de nulos (retorno lista vacía). | No. | Pasó |
| **CU-05** | Confirmar cita por DNI y fecha. | Método `confirmarCita()` (return true). | No. | Pasó |
| **CU-05** | Actualizar estado a 'Confirmado'. | Lógica de búsqueda y actualización de estado. | No. | Pasó |
| **CU-05** | Devolver false en cita inexistente. | Manejo de nulos en `consultarCita` y `confirmarCita`. | No. | Pasó |
| **CU-05** | Devolver false en cita no-pendiente. | Validación de estado en `consultarCita`. | No. | Pasó |

### Ciclo REFACTOR (Mejora de Código)

| ID CU | Objetos Afectados | Por qué refactorizamos | Resultados |
| :--- | :--- | :--- | :--- |
| **CU-01** | Clase `ServicioAgenda` | **Magic Strings:** Reemplazo de textos literales por constantes privadas. | Evita errores de escritura. Tests OK. |
| **CU-02** | Clase `ServicioAgenda` | **Magic Numbers:** Reemplazo de `8` por `HorasMinimasParaReagendar`. | Mejora legibilidad. Tests OK. |
| **CU-02** | `AgendarCita`, `ReagendarCita` | **DRY:** Ambos métodos duplicaban la validación de fecha ocupada. Se extrajo método privado `EsHorarioDisponible`. | Código más limpio y mantenible. Tests OK. |
| **CU-03** | Clases `Turno` y `EstadoTurno` | Coordinación de nomenclatura con el equipo. | Tests siguen funcionando. |
| **CU-04** | Clase `Usuario` | Constructor inicia DNI vacío para evitar nulos. | Tests siguen funcionando. |
| **CU-03/04/05** | Clases Test y `Agenda` | **Repositorio:** Implementación de repositorio para manejo de lista de citas. | Tests fallaron (se actualizaron para usar repositorio). |

