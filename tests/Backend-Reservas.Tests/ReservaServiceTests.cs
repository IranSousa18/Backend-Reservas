using Backend_Reservas.Application.DTOs.Reserva;
using Backend_Reservas.Application.Exceptions;
using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Application.Services;
using Backend_Reservas.Domain.Enums;
using Backend_Reservas.Infrastructure.Repositories;

namespace Backend_Reservas.Tests;

public class ReservaServiceTests
{
    private readonly IReservaRepository _reservaRepository;
    private readonly ISalaRepository _salaRepository;
    private readonly ReservaService _service;

    public ReservaServiceTests()
    {
        _reservaRepository = new ReservaRepository();
        _salaRepository = new SalaRepository();

        _salaRepository.AdicionarAsync(
            new Backend_Reservas.Domain.Entities.Sala
            {
                Nome = "Sala de Reunião",
                Localizacao = "1º Andar",
                Capacidade = 10
            }).GetAwaiter().GetResult();

        _service = new ReservaService(
            _reservaRepository,
            _salaRepository);
    }

    [Fact]
    public async Task CriarAsync_DeveCriarReserva_QuandoNaoExisteConflito()
    {
        var dto = CriarDto(10, 12);

        var resultado = await _service.CriarAsync(dto);

        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.SalaId);
        Assert.Equal("João", resultado.Responsavel);
        Assert.Equal(StatusReserva.Confirmada, resultado.Status);
    }

    [Fact]
    public async Task CriarAsync_DevePermitirReservasConsecutivas()
    {
        await _service.CriarAsync(CriarDto(10, 12));

        var segunda = CriarDto(12, 14);

        var resultado = await _service.CriarAsync(segunda);

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Id);
    }

    [Fact]
    public async Task CriarAsync_DeveRejeitarConflitoNoInicio()
    {
        await _service.CriarAsync(CriarDto(10, 12));

        var conflito = CriarDto(9, 11);

        await Assert.ThrowsAsync<ReservaConflitanteException>(
            () => _service.CriarAsync(conflito));
    }

    [Fact]
    public async Task CriarAsync_DeveRejeitarConflitoNoFinal()
    {
        await _service.CriarAsync(CriarDto(10, 12));

        var conflito = CriarDto(11, 13);

        await Assert.ThrowsAsync<ReservaConflitanteException>(
            () => _service.CriarAsync(conflito));
    }

    [Fact]
    public async Task CriarAsync_DeveRejeitarReservaDentroDeOutra()
    {
        await _service.CriarAsync(CriarDto(10, 14));

        var conflito = CriarDto(11, 12);

        await Assert.ThrowsAsync<ReservaConflitanteException>(
            () => _service.CriarAsync(conflito));
    }

    [Fact]
    public async Task CriarAsync_DeveRejeitarReservaQueEnglobaOutra()
    {
        await _service.CriarAsync(CriarDto(11, 12));

        var conflito = CriarDto(10, 14);

        await Assert.ThrowsAsync<ReservaConflitanteException>(
            () => _service.CriarAsync(conflito));
    }

    [Fact]
    public async Task CriarAsync_DeveRejeitarQuandoHorarioInvalido()
    {
        var dto = CriarDto(14, 10);

        await Assert.ThrowsAsync<PeriodoReservaInvalidoException>(
            () => _service.CriarAsync(dto));
    }

    [Fact]
    public async Task CriarAsync_DeveRejeitarQuandoSalaNaoExiste()
    {
        var dto = CriarDto(10, 12);
        dto.SalaId = 999;

        await Assert.ThrowsAsync<SalaNaoEncontradaException>(
            () => _service.CriarAsync(dto));
    }

    [Fact]
    public async Task AtualizarAsync_NaoDeveConsiderarAPropriaReservaComoConflito()
    {
        var reserva = await _service.CriarAsync(CriarDto(10, 12));

        var dto = CriarAtualizarDto(10, 12);
        dto.Responsavel = "João Atualizado";

        var resultado = await _service.AtualizarAsync(
            reserva.Id,
            dto);

        Assert.True(resultado);

        var atualizada = await _service.ObterPorIdAsync(reserva.Id);

        Assert.NotNull(atualizada);
        Assert.Equal("João Atualizado", atualizada.Responsavel);
    }

    [Fact]
    public async Task AtualizarAsync_DeveRejeitarConflitoComOutraReserva()
    {
        await _service.CriarAsync(CriarDto(10, 12));

        var segunda = await _service.CriarAsync(
            CriarDto(14, 16));

        var dto = CriarAtualizarDto(11, 13);

        await Assert.ThrowsAsync<ReservaConflitanteException>(
            () => _service.AtualizarAsync(segunda.Id, dto));
    }

    [Fact]
    public async Task CriarAsync_DeveIgnorarReservaCanceladaNoConflito()
    {
        var reserva = await _service.CriarAsync(
            CriarDto(10, 12));

        await _service.ExcluirAsync(reserva.Id);

        var novaReserva = CriarDto(10, 12);

        var resultado = await _service.CriarAsync(novaReserva);

        Assert.NotNull(resultado);
    }

    private static CriarReservaDto CriarDto(
        int inicio,
        int fim)
    {
        var data = new DateTimeOffset(
            2026,
            9,
            6,
            0,
            0,
            0,
            TimeSpan.FromHours(-3));

        return new CriarReservaDto
        {
            SalaId = 1,
            Inicio = data.AddHours(inicio),
            Fim = data.AddHours(fim),
            Responsavel = "João"
        };
    }

    private static AtualizarReservaDto CriarAtualizarDto(
        int inicio,
        int fim)
    {
        var data = new DateTimeOffset(
            2026,
            9,
            6,
            0,
            0,
            0,
            TimeSpan.FromHours(-3));

        return new AtualizarReservaDto
        {
            SalaId = 1,
            Inicio = data.AddHours(inicio),
            Fim = data.AddHours(fim),
            Responsavel = "João"
        };
    }
}