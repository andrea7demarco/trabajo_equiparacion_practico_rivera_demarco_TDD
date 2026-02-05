namespace SistemaAgenda.Api.Models;

public class Cita
{
    private Guid _id;
    private Usuario _usuarioAsignado;
    private DateTime _fecha;
    private EstadoCita _estado;

    public Cita()
    {
        _id = Guid.Empty;
        _usuarioAsignado = new Usuario();
        _estado = EstadoCita.Pendiente;
    }

    public Cita(DateTime fecha, Usuario usuarioAsignado)
    {
        _id = Guid.Empty;
        _usuarioAsignado = usuarioAsignado;
        _fecha = fecha;
        _estado = EstadoCita.Pendiente;
    }
    
    public Guid Id { get => _id ; set => _id = value; }
    public Usuario UsuarioAsignado { get => _usuarioAsignado; set => _usuarioAsignado = value; }
    public DateTime Fecha { get => _fecha; set => _fecha = value; }
    public EstadoCita Estado { get => _estado; set => _estado = value; }
}