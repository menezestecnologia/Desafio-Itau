using API.Itau.Transferencia.Domain.Interfaces;
using API.Itau.Transferencia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Itau.Transferencia.Infrastructure.Repository;

public class TransferenciaRepository(InMemoryContext context) : ITransferenciaRepository
{
    private readonly InMemoryContext _context = context;

    public async Task AdicionarAsync(Domain.Entidades.Transferencia transferencia)
    {
        _context.Transferencias.Add(transferencia);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entidades.Transferencia>> ObterPorContaAsync(string numeroConta) =>
        await _context.Transferencias
            .Where(t => t.ContaOrigem == numeroConta || t.ContaDestino == numeroConta)
            .OrderByDescending(t => t.Data)
            .ToListAsync();
}
