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

        var resultado = agenda.eliminarTurno(cita);

        // Como el turno está pendiente, la operación debería retornar true
        // y el turno debería eliminarse    
        Assert.True(resultado);

        // Si el turno es eliminado correctamente, debe cambiar su
        // estado a cancelado
        Assert.True(cita.Estado == EstadoCita.Cancelado);
    }
}