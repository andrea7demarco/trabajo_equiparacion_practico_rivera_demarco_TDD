using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Persistence;
using SistemaAgenda.Api.Services;

namespace SistemaAgenda.Tests;

// Caso de uso: CU-03 -> Eliminar una cita
public class EliminarCitaTestCase
{
    private readonly AgendaServiceImpl agenda;
    private readonly CitaRepositoryMemoryImpl citaRepository = new();

    public EliminarCitaTestCase()
    {
        agenda = new AgendaServiceImpl(citaRepository);
    }

    [Fact]
    public void TestEliminarTurnoPendiente()
    {
        var cita = new Cita()
        {
            UsuarioAsignado = new Usuario() { Dni = "45060776" },
            Fecha = DateTime.Now.AddDays(3),
            Estado = EstadoCita.Pendiente
        };
        citaRepository.CitasProgramadas.Add(cita);
        citaRepository.CitasProgramadas.Add(cita);

        var resultado = agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha);

        // Como el turno está pendiente, la operación debería retornar true
        // y el turno debería eliminarse    
        Assert.True(resultado.Exito);

        // Si el turno es eliminado correctamente, debe cambiar su
        // estado a cancelado
        Assert.NotNull(resultado.Resultado);
        Assert.True(resultado.Resultado.Estado == EstadoCita.Cancelado);
    }

    [Fact]
    public void TestEliminarTurnoConfirmado()
    {
        var cita = new Cita()
        {
            UsuarioAsignado = new Usuario() { Dni = "45060776" },
            Fecha = DateTime.Now.AddDays(3),
            Estado = EstadoCita.Confirmado
        };
        agenda.AgendarCita(cita);

        var resultado = agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha);
        Assert.False(resultado.Exito);
        Assert.NotNull(resultado.Resultado);
        Assert.Equal(EstadoCita.Confirmado, resultado.Resultado.Estado);
    }

    [Fact]
    public void TestEliminarTurnoProximo()
    {
        var cita = new Cita()
        {
            UsuarioAsignado = new Usuario() { Dni = "45060776" },
            Fecha = DateTime.Now.AddHours(2),
            Estado = EstadoCita.Pendiente
        };
        agenda.AgendarCita(cita);

        var resultado = agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha);
        Assert.False(resultado.Exito);
        Assert.NotNull(resultado.Resultado);
        Assert.Equal(EstadoCita.Pendiente, resultado.Resultado.Estado);

        cita.Fecha = DateTime.Now.AddHours(2).AddSeconds(1);

        var resultadoNuevo = agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha);
        Assert.True(resultadoNuevo.Exito);
    }

    [Fact]
    public void TestEliminarTurnoYaCancelado()
    {
        var cita = new Cita()
        {
            UsuarioAsignado = new Usuario() { Dni = "45060776" },
            Fecha = DateTime.Now.AddDays(3),
            Estado = EstadoCita.Cancelado
        };
        agenda.AgendarCita(cita);

        Assert.False(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha).Exito);
    }

    [Fact]
    public void TestEliminarCitaInexistente()
    {
        var cita = new Cita()
        {
            UsuarioAsignado = new Usuario() { Dni = "45060776" },
            Fecha = DateTime.Now.AddDays(3),
            Estado = EstadoCita.Pendiente
        };

        // Debería devolver false porque aún no lo agregué a la Agenda
        Assert.False(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha).Exito);

        // Agrego a la agenda y pruebo de nuevo
        agenda.AgendarCita(cita);
        Assert.True(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha).Exito);
    }
}