using Microsoft.AspNetCore.Mvc;
using SistemaAgenda.Api.Models;
using SistemaAgenda.Api.Services;

namespace SistemaAgenda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AgendaController : Controller
{
    private readonly IAgendaService agendaService;
    private readonly ILogger<AgendaController> logger;

    public AgendaController(IAgendaService _agendaService, ILogger<AgendaController> _logger)
    {
        agendaService = _agendaService;
        logger = _logger;
    }
    
    [HttpGet("/citas/listar")]
    public IActionResult ListarPorDni(string dni)
    {
        return Ok(agendaService.consultarCitas(dni));
    }

    [HttpGet("/citas/consultar")]
    public IActionResult ConsultarCita(string dni, DateTime fecha)
    {
        var respuesta = agendaService.consultarCita(dni, fecha);
        return (respuesta.Exito) 
                    ? Ok(respuesta.Resultado)
                    : BadRequest(respuesta.Mensaje);
    }

    [HttpPost("/citas/agendar")]
    public IActionResult AgendarCita(string dni, DateTime fecha)
    {
        var respuesta = agendaService.AgendarCita(
            new Cita() { UsuarioAsignado = new Usuario { Dni = dni }, Fecha = fecha }
        );

        logger.LogInformation(respuesta.Mensaje);
        logger.LogInformation(respuesta.Resultado.Fecha.ToString());

        return (respuesta.Exito) 
                    ? Ok(respuesta.Mensaje)
                    : BadRequest(respuesta.Mensaje);
    }

    [HttpPut("/citas/confirmar")]
    public IActionResult ConfirmarCita(string dni, DateTime fecha)
    {
        var respuesta = agendaService.confirmarCita(dni, fecha);
        return (respuesta.Exito) 
                    ? Ok(respuesta.Mensaje)
                    : BadRequest(respuesta.Mensaje);
    }

    [HttpDelete("/citas/eliminar")]
    public IActionResult EliminarCita(string dni, DateTime fecha)
    {
        var respuesta = agendaService.eliminarCita(dni, fecha);
        return (respuesta.Exito)
                    ? Ok(respuesta.Mensaje)
                    : BadRequest(respuesta.Mensaje);
    }

    [HttpPut("/citas/reagendar")]
    public IActionResult ReagendarCita(string dni, DateTime fechaVieja, DateTime fechaNueva)
    {
        var respuestaConsulta = agendaService.consultarCita(dni, fechaVieja);
        if (!respuestaConsulta.Exito || respuestaConsulta.Resultado == null)
            return BadRequest(respuestaConsulta.Mensaje);

        var cita = respuestaConsulta.Resultado;
        var respuestaReagenda = agendaService.ReagendarCita(cita.Id, fechaNueva);
        if (!respuestaReagenda.Exito)
            return BadRequest(respuestaReagenda.Mensaje);

        return Ok(respuestaReagenda.Mensaje);
    }
}