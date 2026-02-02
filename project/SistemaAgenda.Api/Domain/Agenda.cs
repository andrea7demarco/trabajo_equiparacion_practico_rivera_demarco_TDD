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

    public bool eliminarCita(string dni, DateTime fecha)
    {
        var cita = consultarCita(dni, fecha);
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

    public List<Cita> consultarCitas(string dni)
    {
        if (string.IsNullOrEmpty(dni))
            return new List<Cita>();
        if (!string.Equals(_dniUsuarioLogueado, dni, StringComparison.OrdinalIgnoreCase))
            return new List<Cita>();

        return _citasProgramadas.Where(cita => string.Equals(cita.UsuarioAsignado.Dni, dni, StringComparison.OrdinalIgnoreCase))
                                .ToList();
    }

    public Cita? consultarCita(string dni, DateTime fecha)
    {
        return _citasProgramadas.FirstOrDefault(cita => cita.UsuarioAsignado.Dni == dni && cita.Fecha == fecha);
    }

    public bool confirmarCita(string dni, DateTime fecha)
    {
        var citaConsultada = consultarCita(dni, fecha);
        if (citaConsultada == null)
            return false;
        if (citaConsultada.Estado == EstadoCita.Cancelado || citaConsultada.Estado == EstadoCita.Confirmado)
            return false;
            
        citaConsultada.Estado = EstadoCita.Confirmado;
        return true;
    }
}