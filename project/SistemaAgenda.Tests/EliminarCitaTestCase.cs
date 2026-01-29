using SistemaAgenda.Api.Domain;

namespace SistemaAgenda.Tests;

// Caso de uso: CU-03 -> Eliminar una cita
public class EliminarCitaTestCase
{
    [Fact]
    public void TestEliminarTurnoPendiente()
    {
        var agenda = new Agenda();
        var turno = new Turno()
        {
            Estado = EstadoTurno.Pendiente
        };

        var resultado = agenda.eliminarTurno(turno);

        // Como el turno está pendiente, la operación debería retornar true
        // y el turno debería eliminarse    
        Assert.True(resultado);
    }
}