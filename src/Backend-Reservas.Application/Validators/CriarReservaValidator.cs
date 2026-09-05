using Backend_Reservas.Application.DTOs.Reserva;
using FluentValidation;

namespace Backend_Reservas.Application.Validators;

public class CriarReservaValidator : AbstractValidator<CriarReservaDto>
{
    public CriarReservaValidator()
    {
        RuleFor(reserva => reserva.SalaId)
            .GreaterThan(0)
            .WithMessage("O ID da sala deve ser maior que zero.");

        RuleFor(reserva => reserva.Inicio)
            .NotEmpty()
            .WithMessage("O horário de início é obrigatório.");

        RuleFor(reserva => reserva.Fim)
            .NotEmpty()
            .WithMessage("O horário de término é obrigatório.");

        RuleFor(reserva => reserva.Responsavel)
            .NotEmpty()
            .WithMessage("O responsável pela reserva é obrigatório.")
            .MaximumLength(150)
            .WithMessage("O responsável deve ter no máximo 150 caracteres.");
    }
}