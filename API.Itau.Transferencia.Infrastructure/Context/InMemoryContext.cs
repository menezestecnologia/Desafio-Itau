using API.Itau.Transferencia.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API.Itau.Transferencia.Infrastructure.Context;

public class InMemoryContext(DbContextOptions<InMemoryContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Domain.Entidades.Transferencia> Transferencias => Set<Domain.Entidades.Transferencia>();
}
