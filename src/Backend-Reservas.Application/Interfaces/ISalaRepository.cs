using Backend_Reservas.Domain.Entities;

namespace Backend_Reservas.Application.Interfaces;

public interface ISalaRepository
{
    Task<IEnumerable<Sala>> ObterTodasAsync();

    Task<Sala?> ObterPorIdAsync(int id);

    Task<Sala> AdicionarAsync(Sala sala);

    Task AtualizarAsync(Sala sala);

    Task ExcluirAsync(Sala sala);
}