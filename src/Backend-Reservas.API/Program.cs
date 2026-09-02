using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ISalaRepository, SalaRepository>();
builder.Services.AddSingleton<IReservaRepository, ReservaRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();