using Backend_Reservas.Application.DTOs.Reserva;
using Backend_Reservas.Application.Exceptions;
using Backend_Reservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Reservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas([FromQuery] int? salaId = null)
    {
        var reservas = await _reservaService.ObterTodasAsync(salaId);

        if (!reservas.Any())
            return NotFound(new
            {
                mensagem = salaId.HasValue
                    ? $"Nenhuma reserva encontrada para a sala de ID {salaId.Value}."
                    : "Nenhuma reserva cadastrada."
            });

        return Ok(reservas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var reserva = await _reservaService.ObterPorIdAsync(id);

        if (reserva is null)
            return NotFound(new
            {
                mensagem = $"A reserva de ID {id} não foi encontrada."
            });

        return Ok(reserva);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarReservaDto dto)
    {
        try
        {
            var reserva = await _reservaService.CriarAsync(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = reserva.Id },
                reserva);
        }
        catch (SalaNaoEncontradaException ex)
        {
            return NotFound(new
            {
                mensagem = ex.Message
            });
        }
        catch (PeriodoReservaInvalidoException ex)
        {
            return BadRequest(new
            {
                mensagem = ex.Message
            });
        }
        catch (ReservaConflitanteException ex)
        {
            return Conflict(new
            {
                mensagem = ex.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] AtualizarReservaDto dto)
    {
        try
        {
            var atualizada = await _reservaService.AtualizarAsync(id, dto);

            if (!atualizada)
                return NotFound(new
                {
                    mensagem = $"A reserva de ID {id} não foi encontrada."
                });

            return NoContent();
        }
        catch (SalaNaoEncontradaException ex)
        {
            return NotFound(new
            {
                mensagem = ex.Message
            });
        }
        catch (PeriodoReservaInvalidoException ex)
        {
            return BadRequest(new
            {
                mensagem = ex.Message
            });
        }
        catch (ReservaConflitanteException ex)
        {
            return Conflict(new
            {
                mensagem = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluida = await _reservaService.ExcluirAsync(id);

        if (!excluida)
            return NotFound(new
            {
                mensagem = $"A reserva de ID {id} não foi encontrada."
            });

        return NoContent();
    }
}