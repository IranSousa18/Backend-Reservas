UPDATE Reserva
SET Deleted = 1
WHERE Id = @Id
  AND Deleted = 0;