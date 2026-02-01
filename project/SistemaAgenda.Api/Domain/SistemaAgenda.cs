using System;
using System.Collections.Generic; // Necesario para usar List<>
using SistemaAgenda.Models;

namespace SistemaAgenda.Services
{
    public class ServicioAgenda
    {
        // Esta lista actúa como nuestra "Base de Datos" temporal
        private List<DateTime> _turnosAgendados = new List<DateTime>();

        public RespuestaCita AgendarCita(SolicitudCita solicitud)
        {
            //comprueba si el turno ya esta agendado
            if (_turnosAgendados.Contains(solicitud.FechaCita))
            {
                // Si existe, devolvemos error (Para que pase el test de Horario Ocupado)
                return new RespuestaCita
                {
                    Exito = false,
                    Mensaje = "El turno ya no está disponible"
                };
            }

            // 2. GUARDADO: Si no existe, la guardamos en la lista
            _turnosAgendados.Add(solicitud.FechaCita);

            // 3. RESPUESTA DE ÉXITO (Para que pase el test Happy Path)
            return new RespuestaCita
            {
                Exito = true,
                Mensaje = "Cita agendada con éxito",
                Estado = "Pendiente",
                IdCita = Guid.NewGuid() // Generamos un ID único
            };
        }
    }
}