namespace SistemaAgenda.Api.Models
{
    public class RespuestaCita
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
        public Cita? Resultado { get; set; }
    }
}