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
    Cita? consultarCita(string dni, DateTime fecha);

    /// <summary>
    /// Elimina una cita
    /// </summary>
    /// <param name="dni">DNI del usuario asignado a la cita</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <returns>True si se logra eliminar, False en caso contrario</returns>
    bool eliminarCita(string dni, DateTime fecha);

    /// <summary>
    /// Confirma una cita pendiente
    /// </summary>
    /// <param name="dni">DNI del usuario asignado a la cita</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <returns>True si se logra eliminar, False en caso contrario</returns>
    bool confirmarCita(string dni, DateTime fecha);
}