using API.Itau.Transferencia.Application.Services;
using API.Itau.Transferencia.Application.Validadores;
using API.Itau.Transferencia.Domain.Interfaces.Repos;
using API.Itau.Transferencia.Domain.Interfaces.Services;
using API.Itau.Transferencia.Domain.Validadores;
using API.Itau.Transferencia.Infrastructure.Context;
using API.Itau.Transferencia.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace API.Itau.Transferencia;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<InMemoryContext>(options =>
            options.UseInMemoryDatabase("BancoTransferencias"));

        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();

        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();

        // Validadores
        builder.Services.AddScoped<IValidadorTransferencia, ValidadorContaExistente>();
        builder.Services.AddScoped<IValidadorTransferencia, ValidadorLimiteValor>();
        builder.Services.AddScoped<IValidadorTransferencia, ValidadorSaldoSuficiente>();

        builder.Services.AddValidatorsFromAssemblyContaining<ClienteDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<TransferenciaDtoValidator>();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
