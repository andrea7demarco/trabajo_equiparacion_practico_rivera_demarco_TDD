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
}