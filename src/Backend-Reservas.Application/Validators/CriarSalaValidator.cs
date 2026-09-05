using Backend_Reservas.Application.DTOs.Sala;
using FluentValidation;

namespace Backend_Reservas.Application.Validators;

public class CriarSalaValidator : AbstractValidator<CriarSalaDto>
{
    public CriarSalaValidator()
    {
        RuleFor(sala => sala.Nome)
            .NotEmpty()
            .WithMessage("O nome da sala é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome da sala deve ter no máximo 100 caracteres.");

        RuleFor(sala => sala.Localizacao)
            .NotEmpty()
            .WithMessage("A localização da sala é obrigatória.")
            .MaximumLength(200)
            .WithMessage("A localização da sala deve ter no máximo 200 caracteres.");

        RuleFor(sala => sala.Capacidade)
            .GreaterThan(0)
            .WithMessage("A capacidade da sala deve ser maior que zero.");
    }
}