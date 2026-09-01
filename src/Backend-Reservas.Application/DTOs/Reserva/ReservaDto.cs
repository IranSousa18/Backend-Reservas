using Backend_Reservas.Domain.Enums;

namespace Backend_Reservas.Application.DTOs.Reserva;

public class ReservaDto
{
    public int Id { get; set; }

    public int SalaId { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fim { get; set; }

    public string Responsavel { get; set; } = string.Empty;

    public StatusReserva Status { get; set; }
}