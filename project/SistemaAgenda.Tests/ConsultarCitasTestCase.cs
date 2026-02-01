using SistemaAgenda.Api.Domain;

namespace SistemaAgenda.Tests;

// Caso de uso: CU-04 -> Consultar citas
public class ConsultarCitasTestCase
{
    [Fact]
    public void TestConsultarCitasUsuarioSinCitas()
    {
        var agenda = new Agenda()
        {
            CitasProgramadas = new List<Cita>()
        };
        var usuario = new Usuario()
        {
            Dni = "45060776"
        };

        agenda.consultarCitas(usuario.Dni);

        // Como no hay citas, ni de ese usuario ni de nadie más, 
        // la lista debería seguir vacía y no ser nula
        Assert.NotNull(agenda.CitasProgramadas);
        Assert.Empty(agenda.CitasProgramadas);
    }
}