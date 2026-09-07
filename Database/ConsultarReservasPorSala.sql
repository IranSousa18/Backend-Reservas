SELECT
    Id,
    SalaId,
    Inicio,
    Fim,
    Responsavel,
    Status,
    Deleted
FROM Reserva
WHERE SalaId = @SalaId
  AND Deleted = 0
ORDER BY Inicio;