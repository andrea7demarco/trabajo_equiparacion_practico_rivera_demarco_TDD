using SistemaAgenda.Api.Models;

namespace SistemaAgenda.Api.Services;

public interface IAgendaService
{
    /// <summary>
    /// Consulta todas las fechas de un usuario
    /// </summary>
    /// <param name="dni">DNI del usuario a consultar</param>
    /// <returns>Una lista de Cita</returns>
    List<Cita> consultarCitas(string dni);

    /// <summary>
    /// Consulta una cita en particular de un usuario
    /// </summary>
    /// <param name="dni">DNI del usuario a consultar</param>
    /// <param name="fecha">Fecha de la cita a consultar</param>
    /// <returns>La cita consultada o un objeto de cita vacío por defecto en caso de que no exista</returns>
    RespuestaCita consultarCita(string dni, DateTime fecha);

    /// <summary>
    /// Elimina una cita
    /// </summary>
    /// <param name="dni">DNI del usuario asignado a la cita</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <returns>True si se logra eliminar, False en caso contrario</returns>
    RespuestaCita eliminarCita(string dni, DateTime fecha);

    /// <summary>
    /// Confirma una cita pendiente
    /// </summary>
    /// <param name="dni">DNI del usuario asignado a la cita</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <returns>True si se logra eliminar, False en caso contrario</returns>
    RespuestaCita confirmarCita(string dni, DateTime fecha);

    /// <summary>
    /// Agenda una nueva cita
    /// </summary>
    /// <param name="solicitud">Datos de la cita a agendar</param>
    /// <returns>Objeto RespuestaCita con el estado de la petición</returns>
    RespuestaCita AgendarCita(Cita solicitud);

    /// <summary>
    /// Reagenda una cita existente
    /// </summary>
    /// <param name="idCita">ID de la cita a reagendar</param>
    /// <param name="nuevaFecha">Nueva fecha</param>
    /// <returns>Objeto RespuestaCita con el estado de la petición</returns>
    RespuestaCita ReagendarCita(Guid idCita, DateTime nuevaFecha);
}