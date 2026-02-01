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
            Assert.Equal("Pendiente", resultado.Estado);
        }

        [Fact]
        public void TestAgendarCita_HorarioOcupado_DeberiaFallar()
        {
           
            var servicio = new ServicioAgenda();
            var fechaConflictiva = DateTime.Now.AddDays(2);

            // Primero agendamos a Ana (esto debería funcionar)
            servicio.AgendarCita(new SolicitudCita 
            { 
                NombreCliente = "Ana", 
                FechaCita = fechaConflictiva 
            });

            
            // Intentamos agendar a Pedro A LA MISMA HORA
            var solicitudPedro = new SolicitudCita 
            { 
                NombreCliente = "Pedro", 
                FechaCita = fechaConflictiva 
            };
            
            var resultado = servicio.AgendarCita(solicitudPedro);

            //deberia fallar ya q estan en el mismo horario
            Assert.False(resultado.Exito, "Debería fallar porque el turno está ocupado");
            Assert.Equal("El turno ya no está disponible", resultado.Mensaje);
        }
    }


    
}