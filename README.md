# Backend-Reservas

API REST para gerenciamento de salas e reservas de salas de reunião.

## Tecnologias

- .NET 10
- C#
- ASP.NET Core Web API
- FluentValidation
- Swagger / OpenAPI
- xUnit
- SQL Server
- Git / GitHub

## Arquitetura

O projeto utiliza uma arquitetura em camadas:

- **Domain:** entidades e enums.
- **Application:** regras de negócio, DTOs, interfaces, serviços, validações e exceções.
- **Infrastructure:** implementação dos repositórios e persistência dos dados em memória.
- **API:** controllers e configuração da aplicação.
- **Tests:** testes unitários.
- **Database:** scripts T-SQL.

## Funcionalidades

### Salas

- Cadastrar sala
- Listar salas
- Consultar sala por ID
- Atualizar sala
- Excluir sala

Dados da sala:

- Nome
- Localização
- Capacidade

### Reservas

- Criar reserva
- Listar reservas
- Filtrar reservas por sala
- Consultar reserva por ID
- Atualizar reserva
- Excluir reserva

Dados da reserva:

- Sala
- Horário de início
- Horário de término
- Responsável
- Status

## Regras de negócio

- O horário de término deve ser maior que o horário de início.
- A sala informada deve existir.
- Não é permitido criar reservas com conflito de horário na mesma sala.
- Reservas consecutivas são permitidas.
- Reservas canceladas não geram conflito com novas reservas.
- Uma reserva não entra em conflito com ela mesma durante uma atualização.
- Reservas excluídas utilizam exclusão lógica.
- Registros com `Deleted = true` não são exibidos.
- Reservas com status `Cancelada` continuam sendo exibidas para preservar o histórico.

## Status da reserva

A reserva possui dois status:

- `1` - Confirmada
- `2` - Cancelada

O campo `Status` representa a situação da reserva.

O campo `Deleted` é usado separadamente para realizar a exclusão lógica do registro.

## Validações

As validações dos dados de entrada são realizadas com FluentValidation.

São validados:

- ID da sala
- Horário de início
- Horário de término
- Responsável
- Status da reserva

Regras como existência da sala e conflito de horários são tratadas na camada de serviços.

## Endpoints

### Sala

```text
GET    /api/Sala
GET    /api/Sala/{id}
POST   /api/Sala
PUT    /api/Sala/{id}
DELETE /api/Sala/{id}
```

### Reserva

```text
GET    /api/Reserva
GET    /api/Reserva/{id}
POST   /api/Reserva
PUT    /api/Reserva/{id}
DELETE /api/Reserva/{id}
```

Filtro por sala:

```text
GET /api/Reserva?salaId={id}
```

## Testes

O projeto possui testes unitários para as principais regras de negócio das reservas.

Para executar:

```bash
dotnet test
```

Resultado atual:

- 12 testes
- 12 aprovados
- 0 falhas

## Banco de dados

A pasta `Database` contém scripts T-SQL para:

- Criar as tabelas
- Criar reserva
- Consultar reservas por sala
- Atualizar reserva
- Realizar exclusão lógica

Os scripts contemplam os campos `Status` e `Deleted`.

## Tratamento de erros

A API utiliza respostas HTTP adequadas:

- `200 OK`
- `201 Created`
- `204 No Content`
- `400 Bad Request`
- `404 Not Found`
- `409 Conflict`

## Como executar

Na raiz do projeto:

```bash
dotnet restore
dotnet build
dotnet run --project src/Backend-Reservas.API
```

Após iniciar a aplicação, utilize o Swagger para testar os endpoints.

## Estrutura do projeto

```text
Backend-Reservas
├── src
│   ├── Backend-Reservas.Domain
│   ├── Backend-Reservas.Application
│   ├── Backend-Reservas.Infrastructure
│   └── Backend-Reservas.API
├── tests
│   └── Backend-Reservas.Tests
└── Database
```