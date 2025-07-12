using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Validadores
{
    public class ValidadorLimiteValor : IValidadorTransferencia
    {
        public Task<string?> Validar(TransferenciaDto dto, Cliente? origem, Cliente? destino)
        {
            return Task.FromResult(dto.Valor > 10000 ? "Valor acima do limite" : null);
        }
    }
}