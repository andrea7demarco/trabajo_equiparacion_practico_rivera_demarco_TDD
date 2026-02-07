using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Persistence;
using SistemaAgenda.Api.Services;

namespace SistemaAgenda.Tests;

// CU-05 -> Confirmar una cita
public class ConfirmarCitaTestCase
{
    private readonly CitaRepositoryMemoryImpl citaRepository = new();

    [Fact]
    public void TestConfirmarCitaProgramada()
    {
        var usuarioLogueado = new Usuario() { Dni = "45060776" };
        var fechaCita = DateTime.Now.AddDays(4);

        var agenda = new AgendaServiceImpl(citaRepository)
        {
            DniUsuarioLogueado = usuarioLogueado.Dni
        };

        agenda.AgendarCita(new Cita() {
            UsuarioAsignado = new Usuario() { Dni = "23412345" },
            Fecha = DateTime.Now.AddDays(4),
            Estado = EstadoCita.Pendiente 
        });
        agenda.AgendarCita(new Cita() {
            UsuarioAsignado = usuarioLogueado,
            Fecha = DateTime.Now.AddDays(1),
            Estado = EstadoCita.Confirmado 
        });
        agenda.AgendarCita(new Cita(){
            UsuarioAsignado = usuarioLogueado,
            Fecha = new DateTime(2025, 12, 12),
            Estado = EstadoCita.Cancelado
        });
        
        // Cita a confirmar
        agenda.AgendarCita(new Cita(){
            UsuarioAsignado = usuarioLogueado,
            Fecha = fechaCita,
            Estado = EstadoCita.Pendiente
        });

        // Verifico que la cita se confirme
        var resultado = agenda.confirmarCita(usuarioLogueado.Dni, fechaCita);
        Assert.True(resultado.Exito);

        // Verifico que el estado de la cita se modifique a 'Confirmado'
        var cita = agenda.consultarCita(usuarioLogueado.Dni, fechaCita);
        Assert.NotNull(cita.Resultado);
        Assert.Equal(EstadoCita.Confirmado, cita.Resultado.Estado);
    }

    [Fact]
    public void TestConfirmarCitaInexistente()
    {
        var dniUsuarioLogueado = "45060776";
        var fechaCita = DateTime.Now.AddDays(4);
        var agenda = new AgendaServiceImpl(citaRepository)
        {
            DniUsuarioLogueado = dniUsuarioLogueado,
        };

        Assert.False(agenda.confirmarCita(dniUsuarioLogueado, fechaCita).Exito);
    }

    [Fact]
    public void TestConfirmarCitaNoPendiente()
    {
        var dniUsuarioLogueado = "45060776";
        var fechaCitaA = DateTime.Now.AddDays(3);
        var fechaCitaB = DateTime.Now.AddDays(2);

        var agenda = new AgendaServiceImpl(citaRepository)
        {
            DniUsuarioLogueado = dniUsuarioLogueado
        };

        citaRepository.CitasProgramadas = new List<Cita>()
        {
            new Cita() {
                UsuarioAsignado = new Usuario() { Dni = dniUsuarioLogueado },
                Fecha = fechaCitaA,
                Estado = EstadoCita.Cancelado
            },
            new Cita()
            {
                UsuarioAsignado = new Usuario() { Dni = dniUsuarioLogueado },
                Fecha = fechaCitaB,
                Estado = EstadoCita.Confirmado 
            },
        };

        // No deberían confirmarse citas ya confirmadas o canceladas
        Assert.False(agenda.confirmarCita(dniUsuarioLogueado, fechaCitaA).Exito);
        Assert.False(agenda.confirmarCita(dniUsuarioLogueado, fechaCitaB).Exito);
    }
}