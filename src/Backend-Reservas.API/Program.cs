using Backend_Reservas.Application.DTOs.Reserva;
using Backend_Reservas.Application.DTOs.Sala;
using Backend_Reservas.Application.Interfaces;
using Backend_Reservas.Application.Services;
using Backend_Reservas.Application.Validators;
using Backend_Reservas.Infrastructure.Repositories;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ISalaRepository, SalaRepository>();
builder.Services.AddSingleton<IReservaRepository, ReservaRepository>();

builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddScoped<IValidator<CriarSalaDto>, CriarSalaValidator>();
builder.Services.AddScoped<IValidator<AtualizarSalaDto>, AtualizarSalaValidator>();
builder.Services.AddScoped<IValidator<CriarReservaDto>, CriarReservaValidator>();
builder.Services.AddScoped<IValidator<AtualizarReservaDto>, AtualizarReservaValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();