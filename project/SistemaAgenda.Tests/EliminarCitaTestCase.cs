using SistemaAgenda.Api.Domain;

namespace SistemaAgenda.Tests;

// Caso de uso: CU-03 -> Eliminar una cita
public class EliminarCitaTestCase
{
    [Fact]
    public void TestEliminarTurnoPendiente()
    {
        var agenda = new Agenda();
        var cita = new Cita()
        {
            Estado = EstadoCita.Pendiente
        };

        var resultado = agenda.eliminarCita(cita);

        // Como el turno está pendiente, la operación debería retornar true
        // y el turno debería eliminarse    
        Assert.True(resultado);

        // Si el turno es eliminado correctamente, debe cambiar su
        // estado a cancelado
        Assert.True(cita.Estado == EstadoCita.Cancelado);
    }

    [Fact]
    public void TestEliminarTurnoCancelado()
    {
        var agenda = new Agenda();
        var cita = new Cita()
        {
            Estado = EstadoCita.Confirmado
        };

        Assert.False(agenda.eliminarCita(cita));
    }

    [Fact]
    public void TestEliminarTurnoProximo()
    {
        var agenda = new Agenda();
        var cita = new Cita()
        {
            Fecha = DateTime.Now.AddHours(2),
            Estado = EstadoCita.Pendiente
        };

        Assert.False(agenda.eliminarCita(cita));

        cita.Fecha = DateTime.Now.AddHours(2).AddSeconds(1);
        
        Assert.True(agenda.eliminarCita(cita));
    }
}