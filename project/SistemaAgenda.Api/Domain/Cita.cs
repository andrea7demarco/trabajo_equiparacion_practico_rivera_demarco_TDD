namespace SistemaAgenda.Api.Domain;

public class Cita
{
    public Usuario UsuarioAsignado { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoCita Estado { get; set; }
}