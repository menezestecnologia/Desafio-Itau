using API.Itau.Transferencia.Domain.Entidades;
using API.Itau.Transferencia.Domain.Interfaces.Repos;
using API.Itau.Transferencia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Itau.Transferencia.Infrastructure.Repository;

public class ClienteRepository(InMemoryContext context) : IClienteRepository
{
    private readonly InMemoryContext _context = context;

    public async Task AdicionarAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cliente>> ListarAsync() => await _context.Clientes.Include(t => t.Transferencias).ToListAsync();

    public async Task<Cliente?> ObterPorNumeroContaAsync(string numeroConta) =>
        await _context.Clientes.Include(t => t.Transferencias).FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);

    public async Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }
}
