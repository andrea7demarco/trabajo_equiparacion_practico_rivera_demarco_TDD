using System;
using System.Collections.Generic; // Necesario para usar List<>
using SistemaAgenda.Models;
using SistemaAgenda.Api.Models;

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

        private const string MENSAJE_CITA_NO_ENCONTRADA = "Cita no encontrada"; //no existe
        private const int HORAS_MINIMAS_PARA_REAGENDAR = 8;
<<<<<<< HEAD
        
=======
>>>>>>> 2b7d8caa7d17d77f039fbd9e48696d4bec24bc26
        private Dictionary<Guid,DateTime> _turnosAgendados = new Dictionary<Guid,DateTime>();

        public RespuestaCita AgendarCita(SolicitudCita solicitud)
        {
            if (EsFechaOcupada(solicitud.FechaCita))
            {
                return CrearRespuestaFallida(MENSAJE_TURNO_NO_DISPONIBLE);
            }

            var id = GuardarNuevoTurno(solicitud.FechaCita);

            return CrearRespuestaExitosa(id, MENSAJE_EXITO);
        }

        public RespuestaCita ReagendarCita(Guid idCita, DateTime nuevaFecha)
        {
            // 1. Validar existencia
            if (!ExisteCita(idCita))
            {
                return CrearRespuestaFallida(MENSAJE_CITA_NO_ENCONTRADA);
            }

            // 2. Validar tiempo (Regla de las 8 horas)
            var fechaOriginal = ObtenerFecha(idCita);
            if (EsTardeParaCambios(fechaOriginal))
            {
                return CrearRespuestaFallida(MENSAJE_ERROR_TIEMPO);
            }

            // 3. Validar disponibilidad
            if (EsFechaOcupada(nuevaFecha))
            {
                return CrearRespuestaFallida(MENSAJE_TURNO_NO_DISPONIBLE);
            }

            // 4. Actualizar
            ActualizarFechaCita(idCita, nuevaFecha);

            return CrearRespuestaExitosa(idCita, MENSAJE_REAGENDA_EXITO);
        }


        private bool EsFechaOcupada(DateTime fecha)
        {
            return _turnosAgendados.ContainsValue(fecha);
        }

        private bool ExisteCita(Guid id)
        {
            return _turnosAgendados.ContainsKey(id);
        }

        private DateTime ObtenerFecha(Guid id)
        {
            return _turnosAgendados[id];
        }

        private bool EsTardeParaCambios(DateTime fechaOriginal)
        {
            var horasRestantes = (fechaOriginal - DateTime.Now).TotalHours;
            return horasRestantes <= HORAS_MINIMAS_PARA_REAGENDAR;
        }

        private Guid GuardarNuevoTurno(DateTime fecha)
        {
            var id = Guid.NewGuid();
            _turnosAgendados.Add(id, fecha);
            return id;
        }

        private void ActualizarFechaCita(Guid id, DateTime nuevaFecha)
        {
            _turnosAgendados[id] = nuevaFecha;
        }

        // --- Fábricas de Respuestas ---

        private RespuestaCita CrearRespuestaExitosa(Guid id, string mensaje)
        {
            return new RespuestaCita
            {
                Exito = true,
                Mensaje = mensaje,
                Estado = ESTADO_PENDIENTE,
                IdCita = id
            };
        }

        private RespuestaCita CrearRespuestaFallida(string mensajeError)
        {
            return new RespuestaCita
            {
                Exito = false,
                Mensaje = mensajeError
            };
        }


        

       


        
    }
}