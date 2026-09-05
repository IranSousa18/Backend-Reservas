using Backend_Reservas.Application.DTOs.Sala;
using Backend_Reservas.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Reservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalaController : ControllerBase
{
    private readonly ISalaService _salaService;
    private readonly IValidator<CriarSalaDto> _criarSalaValidator;
    private readonly IValidator<AtualizarSalaDto> _atualizarSalaValidator;

    public SalaController(
        ISalaService salaService,
        IValidator<CriarSalaDto> criarSalaValidator,
        IValidator<AtualizarSalaDto> atualizarSalaValidator)
    {
        _salaService = salaService;
        _criarSalaValidator = criarSalaValidator;
        _atualizarSalaValidator = atualizarSalaValidator;
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
        var resultado = await _criarSalaValidator.ValidateAsync(dto);

        if (!resultado.IsValid)
        {
            return BadRequest(new
            {
                mensagem = "Os dados da sala são inválidos.",
                erros = resultado.Errors.Select(erro => erro.ErrorMessage)
            });
        }

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
        var resultado = await _atualizarSalaValidator.ValidateAsync(dto);

        if (!resultado.IsValid)
        {
            return BadRequest(new
            {
                mensagem = "Os dados da sala são inválidos.",
                erros = resultado.Errors.Select(erro => erro.ErrorMessage)
            });
        }

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