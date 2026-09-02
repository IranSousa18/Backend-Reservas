using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Domain.Entities;

namespace Backend_Reservas.Infrastructure.Repositories;

public class SalaRepository : ISalaRepository
{
    private readonly List<Sala> _salas = new();

    private int _proximoId = 1;

    public Task<IEnumerable<Sala>> ObterTodasAsync()
    {
        return Task.FromResult<IEnumerable<Sala>>(_salas);
    }

    public Task<Sala?> ObterPorIdAsync(int id)
    {
        var sala = _salas.FirstOrDefault(s => s.Id == id);

        return Task.FromResult(sala);
    }

    public Task<Sala> AdicionarAsync(Sala sala)
    {
        sala.Id = _proximoId++;

        _salas.Add(sala);

        return Task.FromResult(sala);
    }

    public Task AtualizarAsync(Sala sala)
    {
        var index = _salas.FindIndex(s => s.Id == sala.Id);

        if (index >= 0)
        {
            _salas[index] = sala;
        }

        return Task.CompletedTask;
    }

    public Task ExcluirAsync(Sala sala)
    {
        _salas.Remove(sala);

        return Task.CompletedTask;
    }
}