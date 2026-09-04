using Backend_Reservas.Application.DTOs.Reserva;
using Backend_Reservas.Application.Exceptions;
using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Domain.Entities;

namespace Backend_Reservas.Application.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly ISalaRepository _salaRepository;

    public ReservaService(
        IReservaRepository reservaRepository,
        ISalaRepository salaRepository)
    {
        _reservaRepository = reservaRepository;
        _salaRepository = salaRepository;
    }

    public async Task<IEnumerable<ReservaDto>> ObterTodasAsync(int? salaId = null)
    {
        var reservas = await _reservaRepository.ObterTodasAsync(salaId);

        return reservas.Select(reserva => new ReservaDto
        {
            Id = reserva.Id,
            SalaId = reserva.SalaId,
            Inicio = reserva.Inicio,
            Fim = reserva.Fim,
            Responsavel = reserva.Responsavel,
            Status = reserva.Status
        });
    }

    public async Task<ReservaDto?> ObterPorIdAsync(int id)
    {
        var reserva = await _reservaRepository.ObterPorIdAsync(id);

        if (reserva is null)
            return null;

        return new ReservaDto
        {
            Id = reserva.Id,
            SalaId = reserva.SalaId,
            Inicio = reserva.Inicio,
            Fim = reserva.Fim,
            Responsavel = reserva.Responsavel,
            Status = reserva.Status
        };
    }

    public async Task<ReservaDto> CriarAsync(CriarReservaDto dto)
    {
        var sala = await _salaRepository.ObterPorIdAsync(dto.SalaId);

        if (sala is null)
            throw new SalaNaoEncontradaException(dto.SalaId);

        var reserva = new Reserva
        {
            SalaId = dto.SalaId,
            Inicio = dto.Inicio,
            Fim = dto.Fim,
            Responsavel = dto.Responsavel
        };

        await _reservaRepository.AdicionarAsync(reserva);

        return new ReservaDto
        {
            Id = reserva.Id,
            SalaId = reserva.SalaId,
            Inicio = reserva.Inicio,
            Fim = reserva.Fim,
            Responsavel = reserva.Responsavel,
            Status = reserva.Status
        };
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarReservaDto dto)
    {
        var reserva = await _reservaRepository.ObterPorIdAsync(id);

        if (reserva is null)
            return false;

        var sala = await _salaRepository.ObterPorIdAsync(dto.SalaId);

        if (sala is null)
            throw new SalaNaoEncontradaException(dto.SalaId);

        reserva.SalaId = dto.SalaId;
        reserva.Inicio = dto.Inicio;
        reserva.Fim = dto.Fim;
        reserva.Responsavel = dto.Responsavel;

        await _reservaRepository.AtualizarAsync(reserva);

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var reserva = await _reservaRepository.ObterPorIdAsync(id);

        if (reserva is null)
            return false;

        await _reservaRepository.ExcluirAsync(reserva);

        return true;
    }
}