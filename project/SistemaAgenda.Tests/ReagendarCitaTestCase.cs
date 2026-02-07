using SistemaAgenda.Api.Services;
using SistemaAgenda.Api.Models;

namespace SistemaAgenda.Tests
{
    public class ReagendarCitaTestCase
    {
        [Fact]
        public void TestReagendarCita_FlujoNormal()
        {
            var servicio = new AgendaServiceImpl();
            var fechaOriginal = DateTime.Now.AddDays(2); // Pasado mañana
            var fechaNueva = DateTime.Now.AddDays(3);    // Tres días después

            var solicitud = new Cita
            { 
                UsuarioAsignado = new Usuario() { Dni = "44553408" }, 
                Fecha = fechaOriginal 
            };

            var respuestaAgendar = servicio.AgendarCita(solicitud);
            Assert.NotNull(respuestaAgendar.Resultado);

            var resultado = servicio.ReagendarCita(respuestaAgendar.Resultado.Id, fechaNueva);
            Assert.Equal("Turno reagendado", resultado.Mensaje);
            Assert.True(resultado.Exito);
        }

        [Fact]
        public void TestReagendarCita_MenosDe8Horas_DeberiaFallar()
        {
            
            var servicio = new AgendaServiceImpl();
            // Creo una cita que es dentro de 2 horas (menos de 8)
            var fechaCercana = DateTime.Now.AddHours(2); 
            
            var solicitud = new Cita {
                 UsuarioAsignado = new Usuario() { Dni = "Urgente" }, 
                 Fecha = fechaCercana 
            };
            var respuestaAgendar = servicio.AgendarCita(solicitud);
            Assert.NotNull(respuestaAgendar.Resultado);

            var resultado = servicio.ReagendarCita(respuestaAgendar.Resultado.Id, DateTime.Now.AddDays(1));
            Assert.False(resultado.Exito);
            Assert.Equal("No se puede reagendar con menos de 8 horas de anticipación", resultado.Mensaje);
        }

    }
}