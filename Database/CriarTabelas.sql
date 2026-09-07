CREATE TABLE Sala
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    Localizacao NVARCHAR(200) NOT NULL,
    Capacidade INT NOT NULL
);

CREATE TABLE Reserva
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SalaId INT NOT NULL,
    Inicio DATETIMEOFFSET NOT NULL,
    Fim DATETIMEOFFSET NOT NULL,
    Responsavel NVARCHAR(150) NOT NULL,
    Status INT NOT NULL DEFAULT 1,
    Deleted BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_Reserva_Sala
        FOREIGN KEY (SalaId)
        REFERENCES Sala(Id),

    CONSTRAINT CK_Reserva_Periodo
        CHECK (Fim > Inicio),

    CONSTRAINT CK_Reserva_Status
        CHECK (Status IN (1, 2))
);