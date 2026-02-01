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

        List<Cita> resultado = agenda.consultarCitas(usuario.Dni);

        // Como no hay citas, ni de ese usuario ni de nadie más, 
        // la lista debería seguir vacía y no ser nula
        Assert.NotNull(resultado);
        Assert.Empty(resultado);

        // Agrego citas a la lista de programadas pero no asignadas
        // al usuario en cuestión que realiza la consulta
        agenda.CitasProgramadas.Add(new Cita()
        {
           UsuarioAsignado = new Usuario() { Dni = "26012488" },
           Fecha = DateTime.Now.AddDays(2),
           Estado = EstadoCita.Confirmado 
        });
        agenda.CitasProgramadas.Add(new Cita()
        {
           UsuarioAsignado = new Usuario() { Dni = "23412345" },
           Fecha = DateTime.Now.AddDays(4),
           Estado = EstadoCita.Pendiente 
        });

        resultado = agenda.consultarCitas(usuario.Dni);
        Assert.Empty(resultado);
    }
}