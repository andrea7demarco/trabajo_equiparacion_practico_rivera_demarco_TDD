using Xunit;
using SistemaAgenda.Services;
using SistemaAgenda.Models;
using System;

namespace SistemaAgenda.Tests
{
    public class ReagendarCitaTestCase
    {
        [Fact]
        public void TestReagendarCita_FlujoNormal()
        {
            var servicio = new ServicioAgenda();
            var fechaOriginal = DateTime.Now.AddDays(2); // Pasado mañana
            var fechaNueva = DateTime.Now.AddDays(3);    // Tres días después

            var solicitud = new SolicitudCita 
            { 
                NombreCliente = "Maria", 
                FechaCita = fechaOriginal 
            };
            var respuestaAgendar = servicio.AgendarCita(solicitud);
            var idCita = respuestaAgendar.IdCita; 

           //completar dsps
            var resultado = servicio.ReagendarCita(idCita, fechaNueva);

            
            Assert.True(resultado.Exito);
            Assert.Equal("Turno reagendado", resultado.Mensaje);
        }

        [Fact]
        public void TestReagendarCita_MenosDe8Horas()
        {
            
            var servicio = new ServicioAgenda();
            // Creo una cita que es dentro de 2 horas (menos de 8)
            var fechaCercana = DateTime.Now.AddHours(2); 
            
            var solicitud = new SolicitudCita { NombreCliente = "Urgente", FechaCita = fechaCercana };
            var respuestaAgendar = servicio.AgendarCita(solicitud);
            var idCita = respuestaAgendar.IdCita;

            // cambio para maniana 
            var resultado = servicio.ReagendarCita(idCita, DateTime.Now.AddDays(1));

            
            Assert.False(resultado.Exito);
            Assert.Equal("No se puede reagendar con menos de 8 horas de anticipación", resultado.Mensaje);
        }

    }
}