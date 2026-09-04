namespace Backend_Reservas.Application.Exceptions;

public class SalaNaoEncontradaException : Exception
{
    public SalaNaoEncontradaException(int salaId)
        : base($"A sala de ID {salaId} não foi encontrada.")
    {
    }
}