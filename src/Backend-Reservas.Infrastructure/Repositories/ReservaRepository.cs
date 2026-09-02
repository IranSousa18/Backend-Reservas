using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Domain.Entities;

namespace Backend_Reservas.Infrastructure.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly List<Reserva> _reservas = new();

    private int _proximoId = 1;

    public Task<IEnumerable<Reserva>> ObterTodasAsync(int? salaId = null)
    {
        IEnumerable<Reserva> reservas = _reservas;

        if (salaId.HasValue)
        {
            reservas = reservas.Where(r => r.SalaId == salaId.Value);
        }

        return Task.FromResult(reservas);
    }

    public Task<Reserva?> ObterPorIdAsync(int id)
    {
        var reserva = _reservas.FirstOrDefault(r => r.Id == id);

        return Task.FromResult(reserva);
    }

    public Task<Reserva> AdicionarAsync(Reserva reserva)
    {
        reserva.Id = _proximoId++;

        _reservas.Add(reserva);

        return Task.FromResult(reserva);
    }

    public Task AtualizarAsync(Reserva reserva)
    {
        var index = _reservas.FindIndex(r => r.Id == reserva.Id);

        if (index >= 0)
        {
            _reservas[index] = reserva;
        }

        return Task.CompletedTask;
    }

    public Task ExcluirAsync(Reserva reserva)
    {
        _reservas.Remove(reserva);

        return Task.CompletedTask;
    }
}