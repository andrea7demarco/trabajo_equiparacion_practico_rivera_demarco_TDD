namespace SistemaAgenda.Api.Domain;

public class Agenda
{
    public bool eliminarCita(Cita turno)
    {
        if (turno.Estado == EstadoCita.Confirmado)
            return false;

        turno.Estado = EstadoCita.Cancelado;
        return true;
    }
}