using SistemaAgenda.Api.Models;

namespace SistemaAgenda.Api.Persistence;

public interface ICitaRepository
{
    /// <summary>
    /// Elimina una cita de la persistencia
    /// </summary>
    /// <param name="cita">Referencia de la Cita a eliminar</param>
    /// <returns>True si puede eliminarse, False en caso contrario</returns>
    bool Eliminar(Cita cita);
    
    /// <summary>
    /// Permite obtener todas las citas de un usuario
    /// </summary>
    /// <param name="dni">DNI del usuario a consultar</param>
    /// <returns>Lista de Cita</returns>
    List<Cita> ObtenerPorUsuario(string dni);

    /// <summary>
    /// Permite obtener una cita particular de un usuario
    /// </summary>
    /// <param name="dni">DNI del usuario a consultar</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <returns>El objeto Cita con los datos de la cita encontrada, o un objeto vacío si no se encuentra</returns>
    Cita? ObtenerPorUsuario(string dni, DateTime fecha);

    /// <summary>
    /// Permite obtener una cita por su ID
    /// </summary>
    /// <param name="id">ID de la cita</param>
    /// <returns>El objeto cita con los datos de la cita encontrada, o un objeto vacío si no se encuentra</returns>
    Cita? ObtenerPorId(Guid id);

    /// <summary>
    /// Agenda una cita nueva
    /// </summary>
    /// <param name="nuevaCita">Datos de la cita a agendar</param>
    /// <returns>Cita agendada con el ID generado</returns>
    Cita AgendarCita(Cita nuevaCita);

    /// <summary>
    /// Reagenda una cita existente
    /// </summary>
    /// <param name="citaActualizada">Datos de la cita a reagendar</param>
    /// <returns>True si logra reagendarla, False en caso contrario</returns>
    bool ReagendarCita(Cita citaActualizada);

    /// <summary>
    /// Obtiene todas las citas agendadas de todos los usuarios
    /// </summary>
    /// <returns>Lista de citas</returns>
    List<Cita> ObtenerCitas();
}