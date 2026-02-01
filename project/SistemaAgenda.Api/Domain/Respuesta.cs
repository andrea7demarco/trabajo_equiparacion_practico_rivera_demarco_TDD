
using System;
namespace SistemaAgenda.Models
{
public class RespuestaCita
{
    public bool Exito { get; set; }
    public string? Mensaje { get; set; }
    public string? Estado { get; set; }

    public Guid IdCita { get; set; } //agrego

}
}