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

    /// <inheritdoc/>
    public Cita AgendarCita(Cita nuevaCita)
    {
        nuevaCita.Id = Guid.NewGuid();
        _citasProgramadas.Add(nuevaCita);
        return nuevaCita;
    }

    /// <inheritdoc/>
    public bool ReagendarCita(Cita citaActualizada)
    {
        var cita = ObtenerPorId(citaActualizada.Id);
        if (cita == null)
            return false;

        cita.UsuarioAsignado = citaActualizada.UsuarioAsignado;
        cita.Fecha = citaActualizada.Fecha;
        return true;
    }

    /// <inheritdoc/>
    public Cita? ObtenerPorId(Guid id)
    {
        return _citasProgramadas.FirstOrDefault(cita => cita.Id == id);
    }

    /// <inheritdoc/>
    public List<Cita> ObtenerCitas()
    {
        return _citasProgramadas;
    }
}