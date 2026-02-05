//Caso de uso CU-01: Agendar una cita

using Xunit;
using SistemaAgenda.Services; 
using SistemaAgenda.Models;
using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Services;

namespace SistemaAgenda.Tests
{
    public class AgendarCitaTestCase
    {
        [Fact]
        public void TestAgendarCita()
        {
         
            var servicio = new AgendaServiceImpl(); 
            
            var solicitud = new Cita
            {
                UsuarioAsignado = new Usuario() { Dni = "45060776" },
                Fecha = new DateTime(2024, 7, 15, 10, 0, 0),
            };

      
            var resultado = servicio.AgendarCita(solicitud); 

            Assert.True(resultado.Exito); 
            Assert.Equal("Cita agendada con éxito", resultado.Mensaje); 
            Assert.Equal("Pendiente de confirmación", resultado.Estado);
        }

        [Fact]
        public void TestAgendarCita_HorarioOcupado_DeberiaFallar()
        {
           
            var servicio = new AgendaServiceImpl();
            var fechaConflictiva = DateTime.Now.AddDays(2);

            // Primero agendamos a Ana (esto debería funcionar)
            servicio.AgendarCita(new Cita
            { 
                UsuarioAsignado = new Usuario() { Dni = "Ana" }, 
                Fecha = fechaConflictiva 
            });

            
            // Intentamos agendar a Pedro A LA MISMA HORA
            var solicitudPedro = new Cita
            { 
                UsuarioAsignado = new Usuario() { Dni = "Pedro" }, 
                Fecha = fechaConflictiva 
            };
            
            var resultado = servicio.AgendarCita(solicitudPedro);

            //deberia fallar ya q estan en el mismo horario
            Assert.False(resultado.Exito, "Debería fallar porque el turno está ocupado");
            Assert.Equal("El turno ya no está disponible", resultado.Mensaje);
        }
    }


    
}