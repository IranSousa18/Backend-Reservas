namespace Backend_Reservas.Application.Exceptions;

public class ReservaConflitanteException : Exception
{
    public ReservaConflitanteException()
        : base("Já existe uma reserva para esta sala no período informado.")
    {
    }
}