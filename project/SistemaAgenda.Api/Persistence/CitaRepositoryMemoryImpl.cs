using SistemaAgenda.Api.Domain;

namespace SistemaAgenda.Api.Persistence;

public class CitaRepositoryMemoryImpl : ICitaRepository
{
    private List<Cita> _citasProgramadas = new();

    public List<Cita> CitasProgramadas
    {
        get => _citasProgramadas;
        set => _citasProgramadas = value;
    }

    public bool Eliminar(Cita cita)
    {
        return _citasProgramadas.Remove(cita);
    }

    public List<Cita> ObtenerPorUsuario(string dni)
    {
        return _citasProgramadas.Where(cita => cita.UsuarioAsignado.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Cita? ObtenerPorUsuario(string dni, DateTime fecha)
    {
        return _citasProgramadas.FirstOrDefault(cita => cita.UsuarioAsignado.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase) && cita.Fecha == fecha);
    }
}