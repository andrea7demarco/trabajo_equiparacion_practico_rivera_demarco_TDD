namespace SistemaAgenda.Api.Domain;

public class Agenda
{
    public bool eliminarTurno(Cita turno)
    {
        turno.Estado = EstadoCita.Cancelado;
        return true;
    }
}