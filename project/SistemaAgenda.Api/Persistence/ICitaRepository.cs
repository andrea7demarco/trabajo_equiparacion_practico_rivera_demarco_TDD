using SistemaAgenda.Api.Domain;

namespace SistemaAgenda.Api.Persistence;

public interface ICitaRepository
{
    bool Eliminar(Cita cita);
    List<Cita> ObtenerPorUsuario(string dni);
    Cita? ObtenerPorUsuario(string dni, DateTime fecha);
}