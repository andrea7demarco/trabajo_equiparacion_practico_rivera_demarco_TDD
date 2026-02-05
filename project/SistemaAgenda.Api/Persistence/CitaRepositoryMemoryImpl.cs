using SistemaAgenda.Api.Models;

namespace SistemaAgenda.Api.Persistence;

public class CitaRepositoryMemoryImpl : ICitaRepository
{
    private List<Cita> _citasProgramadas = new();

    public List<Cita> CitasProgramadas
    {
        get => _citasProgramadas;
        set => _citasProgramadas = value;
    }

    /// <inheritdoc/>
    public bool Eliminar(Cita cita)
    {
        return _citasProgramadas.Remove(cita);
    }

    /// <inheritdoc/>
    public List<Cita> ObtenerPorUsuario(string dni)
    {
        return _citasProgramadas.Where(cita => cita.UsuarioAsignado.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <inheritdoc/>
    public Cita? ObtenerPorUsuario(string dni, DateTime fecha)
    {
        return _citasProgramadas.FirstOrDefault(cita => cita.UsuarioAsignado.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase) && cita.Fecha == fecha);
    }
}