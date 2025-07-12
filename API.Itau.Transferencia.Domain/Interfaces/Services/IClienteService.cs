using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ListarAsync();
        Task<Cliente?> ObterPorConta(string numeroConta);
        Task AdicionarAsync(ClienteDto dto);
    }
}