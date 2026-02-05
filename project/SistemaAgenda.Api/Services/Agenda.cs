using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Persistence;

namespace SistemaAgenda.Api.Services;

public class AgendaServiceImpl : IAgendaService
{
    private string _dniUsuarioLogueado;

    private ICitaRepository _citaRepository;

    public AgendaServiceImpl(ICitaRepository citaRepository)
    {
        _dniUsuarioLogueado = string.Empty;
        _citaRepository = citaRepository;
    }

    public string DniUsuarioLogueado
    {
        get => _dniUsuarioLogueado;
        set => _dniUsuarioLogueado = value;
    }

    /// <inheritdoc/>
    public bool eliminarCita(string dni, DateTime fecha)
    {
        var cita = _citaRepository.ObtenerPorUsuario(dni, fecha);
        if (cita == null)
            return false;

        if (cita.Estado == EstadoCita.Confirmado || cita.Estado == EstadoCita.Cancelado)
            return false;

        TimeSpan timeSpan = DateTime.Now - cita.Fecha;
        if (Math.Abs(timeSpan.TotalHours) <= 2)
            return false;

        cita.Estado = EstadoCita.Cancelado;
        return true;
    }

    /// <inheritdoc/>
    public List<Cita> consultarCitas(string dni)
    {
        if (string.IsNullOrEmpty(dni))
            return new List<Cita>();
        if (!string.Equals(_dniUsuarioLogueado, dni, StringComparison.OrdinalIgnoreCase))
            return new List<Cita>();

        return _citaRepository.ObtenerPorUsuario(dni);
    }

    /// <inheritdoc/>
    public Cita? consultarCita(string dni, DateTime fecha)
    {
        return _citaRepository.ObtenerPorUsuario(dni, fecha);
    }

    /// <inheritdoc/>
    public bool confirmarCita(string dni, DateTime fecha)
    {
        var citaConsultada = _citaRepository.ObtenerPorUsuario(dni, fecha);
        if (citaConsultada == null)
            return false;
        if (citaConsultada.Estado == EstadoCita.Cancelado || citaConsultada.Estado == EstadoCita.Confirmado)
            return false;
            
        citaConsultada.Estado = EstadoCita.Confirmado;
        return true;
    }
}