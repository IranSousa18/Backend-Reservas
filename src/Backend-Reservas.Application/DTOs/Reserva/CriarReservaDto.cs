namespace Backend_Reservas.Application.DTOs.Reserva;

public class CriarReservaDto
{
    public int SalaId { get; set; }

    public DateTimeOffset Inicio { get; set; }

    public DateTimeOffset Fim { get; set; }

    public string Responsavel { get; set; } = string.Empty;
}