using System;
using System.Collections.Generic; // Necesario para usar List<>
using SistemaAgenda.Models;

namespace SistemaAgenda.Services
{
    public class ServicioAgenda

    {
        //mensajes para las respuestas
        private const string MENSAJE_EXITO = "Cita agendada con éxito";
        private const string MENSAJE_TURNO_NO_DISPONIBLE = "El turno ya no está disponible";
        private const string ESTADO_PENDIENTE = "Pendiente de confirmación";

        private const string MENSAJE_REAGENDA_EXITO = "Turno reagendado";
        private const string MENSAJE_ERROR_TIEMPO = "No se puede reagendar con menos de 8 horas de anticipación";
        // Esta lista actúa como nuestra "Base de Datos" temporal
        private Dictionary<Guid,DateTime> _turnosAgendados = new Dictionary<Guid,DateTime>();

        public RespuestaCita AgendarCita(SolicitudCita solicitud)
        {
            //comprueba si el turno ya esta agendado
            if (_turnosAgendados.ContainsValue(solicitud.FechaCita))
            {
                // Si existe, devolvemos error (Para que pase el test de Horario Ocupado)
                return new RespuestaCita
                {
                    Exito = false,
                    Mensaje = MENSAJE_TURNO_NO_DISPONIBLE
                };
            }

            //genero el ID para poder guardarlo
            var idCita = Guid.NewGuid();

            // 2. GUARDADO: Si no existe, la guardamos en la lista
            _turnosAgendados.Add(idCita, solicitud.FechaCita);

            // 3. RESPUESTA DE ÉXITO (Para que pase el test Happy Path)
            return new RespuestaCita
            {
                Exito = true,
                Mensaje = MENSAJE_EXITO,
                Estado = ESTADO_PENDIENTE,
                IdCita = idCita //el mismo id q ya guardé
            };
        }

        public RespuestaCita ReagendarCita(Guid idCita, DateTime nuevaFecha)
        {
            //  Obtenemos la fecha actual del turno usando el ID
            // Asumo que el id existe 
            var fechaOriginal = _turnosAgendados[idCita];

            // valido las 8 horas
            if ((fechaOriginal - DateTime.Now).TotalHours <= 8)
            {
                return new RespuestaCita
                {
                    Exito = false,
                    Mensaje = MENSAJE_ERROR_TIEMPO
                };
            }

            // verifico si la nueva fecha ya está ocupada
            if (_turnosAgendados.ContainsValue(nuevaFecha))
            {
                 return new RespuestaCita
                {
                    Exito = false,
                    Mensaje = MENSAJE_TURNO_NO_DISPONIBLE
                };
            }

            // sobreescribo la fecha vieja con la nueva
            _turnosAgendados[idCita] = nuevaFecha;

            return new RespuestaCita
            {
                Exito = true,
                Mensaje = MENSAJE_REAGENDA_EXITO,
                IdCita = idCita
            };
        }


        
    }
}