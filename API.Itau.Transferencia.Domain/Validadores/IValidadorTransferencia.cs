using API.Itau.Transferencia.Domain.DTOs;
using API.Itau.Transferencia.Domain.Entidades;

namespace API.Itau.Transferencia.Domain.Validadores
{
    public interface IValidadorTransferencia
    {
        Task<string?> Validar(TransferenciaDto dto, Cliente? origem, Cliente? destino);
    }
}