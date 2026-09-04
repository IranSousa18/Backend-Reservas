using Backend_Reservas.Application.DTOs.Reserva;
using Backend_Reservas.Application.Exceptions;
using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Domain.Entities;
using Backend_Reservas.Domain.Enums;

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
        if (dto.Inicio >= dto.Fim)
            throw new PeriodoReservaInvalidoException();

        var sala = await _salaRepository.ObterPorIdAsync(dto.SalaId);

        if (sala is null)
            throw new SalaNaoEncontradaException(dto.SalaId);

        var reservas = await _reservaRepository.ObterTodasAsync(dto.SalaId);

        var existeConflito = reservas.Any(reserva =>
            reserva.Status != StatusReserva.Cancelada &&
            reserva.Inicio < dto.Fim &&
            reserva.Fim > dto.Inicio);

        if (existeConflito)
            throw new ReservaConflitanteException();

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

        if (dto.Inicio >= dto.Fim)
            throw new PeriodoReservaInvalidoException();

        var sala = await _salaRepository.ObterPorIdAsync(dto.SalaId);

        if (sala is null)
            throw new SalaNaoEncontradaException(dto.SalaId);

        var reservas = await _reservaRepository.ObterTodasAsync(dto.SalaId);

        var existeConflito = reservas.Any(reservaExistente =>
            reservaExistente.Id != id &&
            reservaExistente.Status != StatusReserva.Cancelada &&
            reservaExistente.Inicio < dto.Fim &&
            reservaExistente.Fim > dto.Inicio);

        if (existeConflito)
            throw new ReservaConflitanteException();

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

        reserva.Status = StatusReserva.Cancelada;

        await _reservaRepository.AtualizarAsync(reserva);

        return true;
    }
}