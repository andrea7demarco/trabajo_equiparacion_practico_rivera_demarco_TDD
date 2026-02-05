using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Persistence;
using SistemaAgenda.Api.Services;

namespace SistemaAgenda.Tests;

// Caso de uso: CU-03 -> Eliminar una cita
public class EliminarCitaTestCase
{
    private readonly Agenda agenda;
    private readonly CitaRepositoryMemoryImpl citaRepository = new();

    public EliminarCitaTestCase()
    {
        agenda = new Agenda(citaRepository);
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

        var resultado = agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha);

        // Como el turno está pendiente, la operación debería retornar true
        // y el turno debería eliminarse    
        Assert.True(resultado);

        // Si el turno es eliminado correctamente, debe cambiar su
        // estado a cancelado
        Assert.True(cita.Estado == EstadoCita.Cancelado);
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
        citaRepository.CitasProgramadas.Add(cita);

        Assert.False(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha));
        Assert.Equal(EstadoCita.Confirmado, cita.Estado);
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
        citaRepository.CitasProgramadas.Add(cita);

        Assert.False(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha));
        Assert.Equal(EstadoCita.Pendiente, cita.Estado);

        cita.Fecha = DateTime.Now.AddHours(2).AddSeconds(1);

        Assert.True(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha));
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
        citaRepository.CitasProgramadas.Add(cita);

        Assert.False(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha));
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
        Assert.False(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha));

        // Agrego a la agenda y pruebo de nuevo
        citaRepository.CitasProgramadas.Add(cita);
        Assert.True(agenda.eliminarCita(cita.UsuarioAsignado.Dni, cita.Fecha));
    }
}