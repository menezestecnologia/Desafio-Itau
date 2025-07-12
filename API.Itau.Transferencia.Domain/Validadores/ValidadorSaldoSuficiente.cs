using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Validadores
{
    public class ValidadorSaldoSuficiente : IValidadorTransferencia
    {
        public Task<string?> Validar(TransferenciaDto dto, Cliente? origem, Cliente? destino)
        {
            return Task.FromResult(origem != null && origem.Saldo < dto.Valor ? "Saldo insuficiente" : null);
        }
    }
}