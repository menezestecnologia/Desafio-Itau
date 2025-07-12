using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Interfaces.Repos
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorNumeroContaAsync(string numeroConta);
        Task<IEnumerable<Cliente>> ListarAsync();
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
    }
}