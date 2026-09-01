using Backend_Reservas.Application.DTOs.Reserva;

namespace Backend_Reservas.Application.Interfaces;

public interface IReservaService
{
    Task<IEnumerable<ReservaDto>> ObterTodasAsync(int? salaId = null);

    Task<ReservaDto?> ObterPorIdAsync(int id);

    Task<ReservaDto> CriarAsync(CriarReservaDto dto);

    Task<bool> AtualizarAsync(int id, AtualizarReservaDto dto);

    Task<bool> ExcluirAsync(int id);
}