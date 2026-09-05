using Backend_Reservas.Application.DTOs.Sala;
using Backend_Reservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Reservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalaController : ControllerBase
{
    private readonly ISalaService _salaService;

    public SalaController(ISalaService salaService)
    {
        _salaService = salaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var salas = await _salaService.ObterTodasAsync();

        if (!salas.Any())
            return NotFound(new
            {
                mensagem = "Nenhuma sala cadastrada."
            });

        return Ok(salas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var sala = await _salaService.ObterPorIdAsync(id);

        if (sala is null)
            return NotFound(new
            {
                mensagem = $"A sala de ID {id} não foi encontrada."
            });

        return Ok(sala);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarSalaDto dto)
    {
        var sala = await _salaService.CriarAsync(dto);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = sala.Id },
            sala);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] AtualizarSalaDto dto)
    {
        var atualizada = await _salaService.AtualizarAsync(id, dto);

        if (!atualizada)
            return NotFound(new
            {
                mensagem = $"A sala de ID {id} não foi encontrada."
            });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluida = await _salaService.ExcluirAsync(id);

        if (!excluida)
            return NotFound(new
            {
                mensagem = $"A sala de ID {id} não foi encontrada."
            });

        return NoContent();
    }
}