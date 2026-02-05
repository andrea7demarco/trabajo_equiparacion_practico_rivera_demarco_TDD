using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Persistence;
using SistemaAgenda.Api.Services;

namespace SistemaAgenda.Tests;

// Caso de uso: CU-04 -> Consultar citas
public class ConsultarCitasTestCase
{

    private readonly CitaRepositoryMemoryImpl citaRepository = new();

    [Fact]
    public void TestConsultarCitasUsuarioSinCitas()
    {
        var agenda = new Agenda(citaRepository);
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
        citaRepository.CitasProgramadas.Add(new Cita()
        {
           UsuarioAsignado = new Usuario() { Dni = "26012488" },
           Fecha = DateTime.Now.AddDays(2),
           Estado = EstadoCita.Confirmado 
        });
        citaRepository.CitasProgramadas.Add(new Cita()
        {
           UsuarioAsignado = new Usuario() { Dni = "23412345" },
           Fecha = DateTime.Now.AddDays(4),
           Estado = EstadoCita.Pendiente 
        });

        resultado = agenda.consultarCitas(usuario.Dni);
        Assert.Empty(resultado);
    }

    [Fact]
    public void TestConsultarCitasUsuarioConCitas()
    {
        Usuario usuarioConsulta = new Usuario() { Dni = "45060776" };
        var agenda = new Agenda(citaRepository)
        {
            DniUsuarioLogueado = usuarioConsulta.Dni
        };

        citaRepository.CitasProgramadas = new List<Cita>()
        {
            new Cita()
            {
                UsuarioAsignado = new Usuario() { Dni = "26012488" },
                Fecha = DateTime.Now.AddDays(2),
                Estado = EstadoCita.Confirmado 
            },
            new Cita() {
                UsuarioAsignado = new Usuario() { Dni = "23412345" },
                Fecha = DateTime.Now.AddDays(4),
                Estado = EstadoCita.Pendiente 
            },
            new Cita()
            {
                UsuarioAsignado = usuarioConsulta,
                Fecha = DateTime.Now.AddDays(1),
                Estado = EstadoCita.Confirmado 
            },
            new Cita()
            {
                UsuarioAsignado = usuarioConsulta,
                Fecha = DateTime.Now.AddDays(4),
                Estado = EstadoCita.Pendiente
            },
            new Cita()
            {
                UsuarioAsignado = usuarioConsulta,
                Fecha = new DateTime(2025, 12, 12),
                Estado = EstadoCita.Confirmado 
            },
        };

        // Se crearon 3 citas con el usuario que consulta, por lo que la lista debería
        // devolver 3 resultados y todos deberían contener los datos del usuario que consulta
        List<Cita> resultado = agenda.consultarCitas(usuarioConsulta.Dni);

        Assert.All(resultado, cita => Assert.Equal(usuarioConsulta.Dni, cita.UsuarioAsignado.Dni));
        Assert.True(resultado.Count() == 3);
    }

    [Fact]
    public void TestConsultarCitasOtroUsuario()
    {
        var usuarioConsulta = new Usuario() { Dni = "45060776" };

        var agenda = new Agenda(citaRepository)
        {
            DniUsuarioLogueado = usuarioConsulta.Dni
        };

        citaRepository.CitasProgramadas = new List<Cita>()
        {
            new Cita()
            {
                UsuarioAsignado = new Usuario() { Dni = "26012488" },
                Fecha = DateTime.Now.AddDays(2),
                Estado = EstadoCita.Confirmado 
            },
            new Cita() {
                UsuarioAsignado = new Usuario() { Dni = "26012488" },
                Fecha = DateTime.Now.AddDays(4),
                Estado = EstadoCita.Pendiente 
            },
            new Cita()
            {
                UsuarioAsignado = usuarioConsulta,
                Fecha = DateTime.Now.AddDays(1),
                Estado = EstadoCita.Confirmado 
            },
            new Cita()
            {
                UsuarioAsignado = usuarioConsulta,
                Fecha = DateTime.Now.AddDays(4),
                Estado = EstadoCita.Pendiente
            },
            new Cita()
            {
                UsuarioAsignado = usuarioConsulta,
                Fecha = new DateTime(2025, 12, 12),
                Estado = EstadoCita.Confirmado 
            },
        };

        // Como el DNI consultado es distinto del DNI del usuario logueado
        // entonces la lista retornada debería ser vacía
        List<Cita> resultado = agenda.consultarCitas("26012488");
        Assert.Empty(resultado);
    }

    [Fact]
    public void TestConsultarDniInvalidos()
    {
        string? dniNulo = null;
        string dniVacio = string.Empty;
        string dniConLetras = "F1341216";
        string dniFormatoInvalido = "45.060.776";

        var agenda = new Agenda(citaRepository);
        citaRepository.CitasProgramadas.Add(new Cita() { 
                    UsuarioAsignado = new Usuario() { Dni = "45060776" }, 
                    Fecha = DateTime.Now.AddDays(1), 
                    Estado = EstadoCita.Pendiente 
                }
        );

        List<Cita> resultado = agenda.consultarCitas(dniNulo);
        Assert.NotNull(resultado);
        Assert.Empty(resultado);

        resultado = agenda.consultarCitas(dniVacio);
        Assert.NotNull(resultado);
        Assert.Empty(resultado);

        resultado = agenda.consultarCitas(dniConLetras);
        Assert.NotNull(resultado);
        Assert.Empty(resultado);

        resultado = agenda.consultarCitas(dniFormatoInvalido);
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }
}