namespace SistemaAgenda.Api.Domain;

public class Agenda
{
    public bool eliminarTurno(Turno turno)
    {
        turno.Estado = EstadoTurno.Cancelado;
        return true;
    }
}