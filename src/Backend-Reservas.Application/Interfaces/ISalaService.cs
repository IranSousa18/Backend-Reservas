using Backend_Reservas.Application.DTOs.Sala;

namespace Backend_Reservas.Application.Interfaces;

public interface ISalaService
{
    Task<IEnumerable<SalaDto>> ObterTodasAsync();

    Task<SalaDto?> ObterPorIdAsync(int id);

    Task<SalaDto> CriarAsync(CriarSalaDto dto);

    Task<bool> AtualizarAsync(int id, AtualizarSalaDto dto);

    Task<bool> ExcluirAsync(int id);
}