using SistemaAgenda.Api.Domain;

namespace SistemaAgenda.Tests;

// CU-05 -> Confirmar una cita
public class ConfirmarCitaTestCase
{
    [Fact]
    public void TestConfirmarCitaProgramada()
    {
        var usuarioLogueado = new Usuario() { Dni = "45060776" };
        var fechaCita = DateTime.Now.AddDays(4);
        var agenda = new Agenda()
        {
            DniUsuarioLogueado = usuarioLogueado.Dni,
            CitasProgramadas = new List<Cita>()
            {
                new Cita() {
                    UsuarioAsignado = new Usuario() { Dni = "23412345" },
                    Fecha = DateTime.Now.AddDays(4),
                    Estado = EstadoCita.Pendiente 
                },
                new Cita()
                {
                    UsuarioAsignado = usuarioLogueado,
                    Fecha = DateTime.Now.AddDays(1),
                    Estado = EstadoCita.Confirmado 
                },
                new Cita()
                {
                    UsuarioAsignado = usuarioLogueado,
                    Fecha = DateTime.Now.AddDays(4),
                    Estado = EstadoCita.Pendiente
                },
                new Cita()
                {
                    UsuarioAsignado = usuarioLogueado,
                    Fecha = new DateTime(2025, 12, 12),
                    Estado = EstadoCita.Cancelado
                },
            }
        };

        Assert.True(agenda.confirmarCita(usuarioLogueado.Dni, fechaCita));
    }
}