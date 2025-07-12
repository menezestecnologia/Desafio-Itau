using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Interfaces
{
    public interface ITransferenciaRepository
    {
        Task<IEnumerable<Entidades.Transferencia>> ObterPorContaAsync(string numeroConta);
        Task AdicionarAsync(Entidades.Transferencia transferencia);
    }
}