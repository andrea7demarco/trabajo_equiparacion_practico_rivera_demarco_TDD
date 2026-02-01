using Microsoft.VisualBasic;

namespace SistemaAgenda.Api.Domain;

public class Agenda
{

    private List<Cita> _citasProgramadas;
    private string _dniUsuarioLogueado;

    public Agenda()
    {
        _citasProgramadas = new List<Cita>();
        _dniUsuarioLogueado = string.Empty;
    }

    public List<Cita> CitasProgramadas
    {
        get => _citasProgramadas;
        set => _citasProgramadas = value;
    }

    public string DniUsuarioLogueado
    {
        get => _dniUsuarioLogueado;
        set => _dniUsuarioLogueado = value;
    }

    public bool eliminarCita(Cita turno)
    {
        if (turno.Estado == EstadoCita.Confirmado || turno.Estado == EstadoCita.Cancelado)
            return false;

        TimeSpan timeSpan = DateTime.Now - turno.Fecha;
        if (Math.Abs(timeSpan.TotalHours) <= 2)
            return false;

        turno.Estado = EstadoCita.Cancelado;
        return true;
    }

    public List<Cita> consultarCitas(string dni)
    {
        if (string.IsNullOrEmpty(dni))
            return new List<Cita>();
        if (!string.Equals(_dniUsuarioLogueado, dni, StringComparison.OrdinalIgnoreCase))
            return new List<Cita>();

        return _citasProgramadas.Where(cita => string.Equals(cita.UsuarioAsignado.Dni, dni, StringComparison.OrdinalIgnoreCase))
                                .ToList();
    }

    public Cita consultarCita(string dni, DateTime fecha)
    {
        return _citasProgramadas.First(cita => cita.UsuarioAsignado.Dni == dni && cita.Fecha == fecha);
    }

    public bool confirmarCita(string dni, DateTime fecha)
    {
        var citaConsultada = consultarCita(dni, fecha);
        citaConsultada.Estado = EstadoCita.Confirmado;
        return true;
    }
}