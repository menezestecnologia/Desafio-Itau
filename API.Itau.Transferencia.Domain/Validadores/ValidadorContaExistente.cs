using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Validadores
{
    public class ValidadorContaExistente : IValidadorTransferencia
    {
        public Task<string?> Validar(TransferenciaDto dto, Cliente? origem, Cliente? destino)
        {
            return Task.FromResult(origem == null || destino == null ? "Conta inexistente" : null);
        }
    }
}