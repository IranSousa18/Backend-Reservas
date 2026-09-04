namespace Backend_Reservas.Application.Exceptions;

public class PeriodoReservaInvalidoException : Exception
{
    public PeriodoReservaInvalidoException()
        : base("O horário de término deve ser maior que o horário de início.")
    {
    }
}