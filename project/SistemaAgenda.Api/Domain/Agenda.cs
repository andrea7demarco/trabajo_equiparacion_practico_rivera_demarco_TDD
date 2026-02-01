namespace SistemaAgenda.Api.Domain;

public class Agenda
{
    public bool eliminarCita(Cita turno)
    {
        if (turno.Estado == EstadoCita.Confirmado)
            return false;

        TimeSpan timeSpan = DateTime.Now - turno.Fecha;
        if (Math.Abs(timeSpan.TotalHours) <= 2)
            return false;

        turno.Estado = EstadoCita.Cancelado;
        return true;
    }
}