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

    }
}