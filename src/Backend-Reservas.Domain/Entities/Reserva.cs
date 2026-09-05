using Backend_Reservas.Domain.Enums;

namespace Backend_Reservas.Domain.Entities;

public class Reserva
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public DateTimeOffset Inicio { get; set; }
    public DateTimeOffset Fim { get; set; }
    public string Responsavel { get; set; } = string.Empty;
    public StatusReserva Status { get; set; } = StatusReserva.Confirmada;
    public bool Deleted { get; set; } = false;
}