namespace Backend_Reservas.Application.DTOs.Reserva;

public class AtualizarReservaDto
{
    public int SalaId { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fim { get; set; }

    public string Responsavel { get; set; } = string.Empty;
}