UPDATE Reserva
SET
    SalaId = @SalaId,
    Inicio = @Inicio,
    Fim = @Fim,
    Responsavel = @Responsavel,
    Status = @Status
WHERE Id = @Id
  AND Deleted = 0;