using Backend_Reservas.Domain.Entities;

namespace Backend_Reservas.Application.Interfaces;

public interface IReservaRepository
{
    Task<IEnumerable<Reserva>> ObterTodasAsync(int? salaId = null);

    Task<Reserva?> ObterPorIdAsync(int id);

    Task<Reserva> AdicionarAsync(Reserva reserva);

    Task AtualizarAsync(Reserva reserva);

    Task ExcluirAsync(Reserva reserva);
}