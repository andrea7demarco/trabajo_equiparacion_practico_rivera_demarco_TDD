namespace SistemaAgenda.Api.Domain;

public class Agenda
{

    private List<Cita> _citasProgramadas;

    public Agenda()
    {
        _citasProgramadas = new List<Cita>();
    }

    public List<Cita> CitasProgramadas
    {
        get => _citasProgramadas;
        set => _citasProgramadas = value ?? new List<Cita>();
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
        return _citasProgramadas;
    }
}