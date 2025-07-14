namespace API.Itau.Transferencia.Domain.Interfaces.Repos
{
    public interface ITransferenciaRepository
    {
        Task<IEnumerable<Entidades.Transferencia>> ListarPorClienteAsync(Guid clienteId);
        Task AdicionarAsync(Entidades.Transferencia transferencia);
    }
}