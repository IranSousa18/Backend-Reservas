namespace Backend_Reservas.Domain.Entities;

public class Sala
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Localizacao { get; set; } = string.Empty;

    public int Capacidade { get; set; }
}