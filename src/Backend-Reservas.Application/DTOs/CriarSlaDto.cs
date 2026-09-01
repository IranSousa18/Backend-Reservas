namespace Backend_Reservas.Application.DTOs.Sala;

public class CriarSalaDto
{
    public string Nome { get; set; } = string.Empty;

    public string Localizacao { get; set; } = string.Empty;

    public int Capacidade { get; set; }
}