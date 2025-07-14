using API.Itau.Transferencia.Domain.DTOs;

namespace API.Itau.Transferencia.Domain.Interfaces.Services
{
    public interface ITransferenciaService
    {
        Task RealizarAsync(TransferenciaDto dto);
        Task<IEnumerable<Entidades.Transferencia>> ListarPorClienteAsync(Guid clienteId);
    }
}