using API.Itau.Transferencia.Domain.Interfaces.Repos;
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

    public async Task<IEnumerable<Domain.Entidades.Transferencia>> ListarPorClienteAsync(Guid clienteId)
    {
        return await _context.Transferencias
            .Where(t => t.ClienteId == clienteId)
            .OrderByDescending(t => t.Data)
            .ToListAsync();
    }
}
