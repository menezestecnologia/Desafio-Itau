using API.Itau.Transferencia.Application.Services;
using API.Itau.Transferencia.Domain.Interfaces;
using API.Itau.Transferencia.Domain.Validadores;
using API.Itau.Transferencia.Infrastructure.Context;
using API.Itau.Transferencia.Infrastructure.Repository;
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
        builder.Services.AddScoped<ClienteService>();
        builder.Services.AddScoped<TransferenciaService>();

        // Validadores
        builder.Services.AddScoped<IValidadorTransferencia, ValidadorContaExistente>();
        builder.Services.AddScoped<IValidadorTransferencia, ValidadorLimiteValor>();
        builder.Services.AddScoped<IValidadorTransferencia, ValidadorSaldoSuficiente>();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
