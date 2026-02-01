namespace SistemaAgenda.Api.Domain;

public class Usuario
{
    private string _dni;

    public Usuario()
    {
        _dni = string.Empty;
    }

    public string Dni
    {
        get => _dni ?? string.Empty;
        set => _dni = value;
    }
}