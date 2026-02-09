using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Persistence;

namespace SistemaAgenda.Api.Services;

public class AgendaServiceImpl : IAgendaService
{
    //mensajes para las respuestas
    private const string MENSAJE_EXITO = "Cita agendada con éxito";
    private const string MENSAJE_CONFIRMACION_EXITOSA = "Cita confirmada con éxto";
    private const string MENSAJE_CITA_NO_DISPONIBLE = "El turno ya no está disponible";

    private const string MENSAJE_REAGENDA_EXITO = "Turno reagendado";
    private const string MENSAJE_ERROR_TIEMPO_REAGENDAR = "No se puede reagendar con menos de 8 horas de anticipación";
    private const string MENSAJE_ERROR_TIEMPO_CANCELACION = "Se debe cancelar con más de 2 horas de anticipación";

    private const string MENSAJE_CITA_NO_ENCONTRADA = "Cita no encontrada"; //no existe

    private const string MENSAJE_CITA_CONFIRMADA = "El turno ya se encuentra cancelado o confirmado";
    private const string MENSAJE_CITA_ELIMINADA = "Se ha eliminado la cita";

    private const int HORAS_MINIMAS_PARA_REAGENDAR = 8;

    private string _dniUsuarioLogueado;
    private ICitaRepository _citaRepository;

    public AgendaServiceImpl()
    {
        _dniUsuarioLogueado = string.Empty;
        _citaRepository = new CitaRepositoryMemoryImpl();
    }

    public AgendaServiceImpl(ICitaRepository citaRepository)
    {
        _dniUsuarioLogueado = string.Empty;
        _citaRepository = citaRepository;
        _citaRepository = citaRepository;
    }

    public string DniUsuarioLogueado
    {
        get => _dniUsuarioLogueado;
        set => _dniUsuarioLogueado = value;
    }

    /// <inheritdoc/>
    public RespuestaCita eliminarCita(string dni, DateTime fecha)
    {
        var cita = _citaRepository.ObtenerPorUsuario(dni, fecha);
        if (cita == null)
            return CrearRespuestaFallida(MENSAJE_CITA_NO_ENCONTRADA);

        if (cita.Estado == EstadoCita.Confirmado || cita.Estado == EstadoCita.Cancelado)
            return CrearRespuestaFallida(MENSAJE_CITA_CONFIRMADA, cita);

        TimeSpan timeSpan = DateTime.Now - cita.Fecha;
        if (Math.Abs(timeSpan.TotalHours) <= 2)
            return CrearRespuestaFallida(MENSAJE_ERROR_TIEMPO_CANCELACION, cita);

        cita.Estado = EstadoCita.Cancelado;
        return CrearRespuestaExitosa(cita, MENSAJE_CITA_ELIMINADA);
    }

    /// <inheritdoc/>
    public List<Cita> consultarCitas(string dni)
    {
        if (string.IsNullOrEmpty(dni))
            return new List<Cita>();
        // if (!string.Equals(_dniUsuarioLogueado, dni, StringComparison.OrdinalIgnoreCase))
        //     return new List<Cita>();

        return _citaRepository.ObtenerPorUsuario(dni);
    }

    /// <inheritdoc/>
    public RespuestaCita consultarCita(string dni, DateTime fecha)
    {
        var cita = _citaRepository.ObtenerPorUsuario(dni, fecha);
        if (cita != null && cita.Id != Guid.Empty)
            return CrearRespuestaExitosa(cita);

        return CrearRespuestaFallida(MENSAJE_CITA_NO_ENCONTRADA);
    }

    /// <inheritdoc/>
    public RespuestaCita confirmarCita(string dni, DateTime fecha)
    {
        var citaConsultada = _citaRepository.ObtenerPorUsuario(dni, fecha);
        if (citaConsultada == null)
            return CrearRespuestaFallida(MENSAJE_CITA_NO_ENCONTRADA);
        if (citaConsultada.Estado == EstadoCita.Cancelado || citaConsultada.Estado == EstadoCita.Confirmado)
            return CrearRespuestaFallida(MENSAJE_CITA_CONFIRMADA, citaConsultada);
            
        citaConsultada.Estado = EstadoCita.Confirmado;
        return CrearRespuestaExitosa(citaConsultada, MENSAJE_CONFIRMACION_EXITOSA);
    }

    /// <inheritdoc/>
    public RespuestaCita AgendarCita(Cita solicitud)
    {
        if (EsFechaOcupada(solicitud.Fecha))
        {
            return CrearRespuestaFallida(MENSAJE_CITA_NO_DISPONIBLE);
        }

        var citaAgendada = _citaRepository.AgendarCita(solicitud);
        // var id = GuardarNuevoTurno(solicitud.Fecha);

        return CrearRespuestaExitosa(citaAgendada, MENSAJE_EXITO);
    }

    /// <inheritdoc/>
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
            return CrearRespuestaFallida(MENSAJE_ERROR_TIEMPO_REAGENDAR);
        }

        // 3. Validar disponibilidad
        if (EsFechaOcupada(nuevaFecha))
        {
            return CrearRespuestaFallida(MENSAJE_CITA_NO_DISPONIBLE);
        }

        // 4. Actualizar
        var cita = _citaRepository.ObtenerPorId(idCita);
        if (cita == null)
            return CrearRespuestaFallida($"No existe cita de id {idCita}");

        cita.Fecha = nuevaFecha;
        _citaRepository.ReagendarCita(cita);
        // ActualizarFechaCita(idCita, nuevaFecha);

        return CrearRespuestaExitosa(cita, MENSAJE_REAGENDA_EXITO);
    }

    private bool EsFechaOcupada(DateTime fecha)
    {
        return _citaRepository.ObtenerCitas().Any(cita => cita.Fecha == fecha);
    }

    private bool ExisteCita(Guid id)
    {
        Cita? cita = _citaRepository.ObtenerPorId(id);
        return cita != null && cita.Id != Guid.Empty;
    }

    private DateTime ObtenerFecha(Guid id)
    {
        var cita = _citaRepository.ObtenerPorId(id);
        if (cita is null)
        {
            throw new Exception($"No existe cita de id {id}");
        }

        return cita.Fecha;        
    }

    private bool EsTardeParaCambios(DateTime fechaOriginal)
    {
        var horasRestantes = (fechaOriginal - DateTime.Now).TotalHours;
        return horasRestantes <= HORAS_MINIMAS_PARA_REAGENDAR;
    }

    private RespuestaCita CrearRespuestaExitosa(Cita cita, string mensaje = "")
    {
        return new RespuestaCita
        {
            Exito = true,
            Mensaje = mensaje,
            Resultado = cita
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

    private RespuestaCita CrearRespuestaFallida(string mensajeError, Cita cita)
    {
        return new RespuestaCita
        {
            Exito = false,
            Mensaje = mensajeError,
            Resultado = cita
        };
    }
}