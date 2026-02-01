//Caso de uso CU-01: Agendar una cita

using Xunit;
using SistemaAgenda.Services; 
using SistemaAgenda.Models;   

namespace SistemaAgenda.Tests
{
    public class AgendarCitaTestCase
    {
        [Fact]
        public void TestAgendarCita()
        {
         
            var servicio = new ServicioAgenda(); 
            
            var solicitud = new SolicitudCita
            {
                NombreCliente = "Juan Perez",
                FechaCita = new DateTime(2024, 7, 15, 10, 0, 0),
            };

      
            var resultado = servicio.AgendarCita(solicitud); 

            Assert.True(resultado.Exito); 
            Assert.Equal("Cita agendada con éxito", resultado.Mensaje); 
            Assert.Equal("Pendiente de confirmacion", resultado.Estado);
        }
    }
}