using Backend_Reservas.Application.DTOs.Sala;
using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Domain.Entities;

namespace Backend_Reservas.Application.Services;

public class SalaService : ISalaService
{
    private readonly ISalaRepository _salaRepository;

    public SalaService(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task<IEnumerable<SalaDto>> ObterTodasAsync()
    {
        var salas = await _salaRepository.ObterTodasAsync();

        return salas.Select(sala => new SalaDto
        {
            Id = sala.Id,
            Nome = sala.Nome,
            Localizacao = sala.Localizacao,
            Capacidade = sala.Capacidade
        });
    }

    public async Task<SalaDto?> ObterPorIdAsync(int id)
    {
        var sala = await _salaRepository.ObterPorIdAsync(id);

        if (sala is null)
            return null;

        return new SalaDto
        {
            Id = sala.Id,
            Nome = sala.Nome,
            Localizacao = sala.Localizacao,
            Capacidade = sala.Capacidade
        };
    }

    public async Task<SalaDto> CriarAsync(CriarSalaDto dto)
    {
        var sala = new Sala
        {
            Nome = dto.Nome,
            Localizacao = dto.Localizacao,
            Capacidade = dto.Capacidade
        };

        await _salaRepository.AdicionarAsync(sala);

        return new SalaDto
        {
            Id = sala.Id,
            Nome = sala.Nome,
            Localizacao = sala.Localizacao,
            Capacidade = sala.Capacidade
        };
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarSalaDto dto)
    {
        var sala = await _salaRepository.ObterPorIdAsync(id);

        if (sala is null)
            return false;

        sala.Nome = dto.Nome;
        sala.Localizacao = dto.Localizacao;
        sala.Capacidade = dto.Capacidade;

        await _salaRepository.AtualizarAsync(sala);

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var sala = await _salaRepository.ObterPorIdAsync(id);

        if (sala is null)
            return false;

        await _salaRepository.ExcluirAsync(sala);

        return true;
    }
}