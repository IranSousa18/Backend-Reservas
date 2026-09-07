INSERT INTO Reserva
(
    SalaId,
    Inicio,
    Fim,
    Responsavel,
    Status,
    Deleted
)
VALUES
(
    @SalaId,
    @Inicio,
    @Fim,
    @Responsavel,
    1,
    0
);